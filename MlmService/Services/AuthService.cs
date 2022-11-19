using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using MlmService.Dto.Auth;
using MlmService.Database;
using MlmService.Helper;
using MlmService.Options;
using MlmService.Database.Models.Core;
using MlmService.Contracts;

namespace MlmService.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;
    private readonly CoreContext _context;
    private readonly TenantContext _tenantContext;
    private readonly ITenantService _tenantService;
    private readonly DbSettings _dbSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(
        JwtSettings jwtSettings, 
        CoreContext context, 
        TenantContext tenantContext,
        ITenantService tenantService,
        DbSettings dbSettings, 
        IHttpContextAccessor httpContextAccessor)
    {
        _jwtSettings = jwtSettings;
        _context = context;
        _tenantContext = tenantContext;
        _tenantService = tenantService;
        _dbSettings = dbSettings;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthenticationResult> RegisterAsync(string username, string password)
    {
        var existingUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Username.ToLower().Trim() == username.ToLower().Trim());
        if (existingUser != null)
        {
            return new AuthenticationResult
            {
                Error = "User with this username already exists"
            };
        }

        var salt = GetSalt();
        var storePassword = password + salt;

        var newUser = new User
        {
            Username = username,
            Password = HashPassword(storePassword),
            Salt = salt,
            IsUsernameLogin = true,
            IsRoot = true,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Tenant = new Tenant
            {
                Id = Guid.NewGuid(),
            },
        };

        try
        {
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
        catch(Exception)
        {
            return new AuthenticationResult
            {
                Error = "Something went wrong"
            };
        }

        _tenantService.SetConnectionString(_dbSettings.BaseConnection + username);
        await _tenantContext.Database.MigrateAsync();

        return await GenerateAuthenticationResultForUserAsync(newUser, true);
    }

    public async Task<AuthenticationResult> LoginAsync(string username, string password)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.IsUsernameLogin && e.Username.ToLower().Trim() == username.ToLower().Trim());
        if (user == null)
        {
            return new AuthenticationResult
            {
                Error = "user does not exist"
            };
        }

        var userHasValidPassword = VerifyPassword(password + user.Salt, user.Password);
        if (!userHasValidPassword)
        {
            return new AuthenticationResult
            {
                Error = "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง"
            };
        }

        return await GenerateAuthenticationResultForUserAsync(user, true);
    }

    public void Logout()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("jwt", string.Empty, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(-1),
        });
    }

    public async Task<AuthenticationResult> RefreshTokenAsync(string token)
    {
        var validatedToken = GetPrincipalFromToken(token, _jwtSettings.RefreshSecret);
        if (validatedToken is null)
        {
            return new AuthenticationResult
            {
                Error = "Invalid Token"
            };
        }

        var user = await _context.Users.Include(e => e.RefreshTokens).FirstOrDefaultAsync(e => e.RefreshTokens.Any(x => x.Token == token));
        if (user == null)
        {
            return new AuthenticationResult
            {
                Error = "This refresh token does not exist"
            };
        }

        var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
        if (refreshToken.Used)
        {
            return new AuthenticationResult
            {
                Error = "This refresh token has been used"
            };
        }

        var updateRefreshToken = await _context.RefreshTokens.FindAsync(refreshToken.Token);
        if (updateRefreshToken != null)
        {
            updateRefreshToken.Used = true;
            await _context.SaveChangesAsync();
        }

        return await GenerateAuthenticationResultForUserAsync(user, true);
    }

    private static string GetSalt()
    {
        byte[] saltBytes = new byte[64];

        var rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(saltBytes);
        var saltText = BitConverter.ToString(saltBytes);

        return saltText;
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 5);
    }

    private static bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    private static ClaimsPrincipal GetPrincipalFromToken(string token, string key)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireExpirationTime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            },
            out var validatedToken);

            if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
            {
                return null;
            }

            return principal;
        }
        catch
        {
            return null;
        }
    }

    private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken)
            && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
    }

    private async Task<AuthenticationResult> GenerateAuthenticationResultForUserAsync(User user, bool isUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var refreshKey = Encoding.ASCII.GetBytes(_jwtSettings.RefreshSecret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Username),
                new Claim("id", user.Id.ToString()),
                new Claim("tenantId", user.TenantId.ToString()),
                new Claim("user", user.Username),
                new Claim(ClaimTypes.Role, isUser ? AccountHelper.User : AccountHelper.Member)
            }),
            Expires = DateTime.Now.Add(_jwtSettings.TokenLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        tokenDescriptor.Expires = DateTime.Now.AddDays(5);
        tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(refreshKey), SecurityAlgorithms.HmacSha256Signature);
        var refresh = tokenHandler.CreateToken(tokenDescriptor);

        var refreshToken = new RefreshToken
        {
            Token = tokenHandler.WriteToken(refresh),
            Used = false,
            JwtId = token.Id,
            UserId = user.Id,
            CreatedDate = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddMonths(6),
        };

        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();

        _httpContextAccessor.HttpContext?.Response.Cookies.Append("jwt", refreshToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(5),
        });

        return new AuthenticationResult
        {
            Success = true,
            Token = tokenHandler.WriteToken(token),
        };
    }
}