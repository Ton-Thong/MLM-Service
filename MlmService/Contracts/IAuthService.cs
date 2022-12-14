using MlmService.Dto.Auth;

namespace MlmService.Contracts;

public interface IAuthService
{
    Task<AuthenticationResult> RegisterAsync(string primaryPhone, string password);
    Task<AuthenticationResult> LoginAsync(string username, string password);
    Task<AuthenticationResult> RefreshTokenAsync(string token);
    void Logout();
}