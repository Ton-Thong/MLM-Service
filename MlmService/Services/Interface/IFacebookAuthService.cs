using MlmService.Dto.Authentication.Facebook;

namespace MlmService.Services.Interface;

public interface IFacebookAuthService
{
    Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
    Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
}