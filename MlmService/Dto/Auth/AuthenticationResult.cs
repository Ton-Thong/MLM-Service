namespace MlmService.Dto.auth;

public class AuthenticationResult
{
    public string Token { get; set; }
    public bool Success { get; set; }
    public string Error { get; set; }
}