using MlmService.Dto.User;

namespace MlmService.Contracts;

public interface IUserService
{
    Task<UserDto> GetUserInfo(Guid userId);
}
