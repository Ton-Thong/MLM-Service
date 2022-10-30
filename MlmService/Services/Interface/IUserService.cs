using MlmService.Dto.User;

namespace MlmService.Services.Interface;

public interface IUserService
{
    Task<UserDto> GetUserInfo(Guid userId);
}
