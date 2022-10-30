using MlmService.Dto.auth;

public interface IAuthService
{
    Task<AuthenticationResult> RegisterAsync(string primaryPhone, string password);
    Task<AuthenticationResult> LoginAsync(string username, string password);
    void LogoutAsync();
    Task<AuthenticationResult> LoginWithFacebookAsync(string accessToken);
    Task<AuthenticationResult> RefreshTokenAsync(string token);
}