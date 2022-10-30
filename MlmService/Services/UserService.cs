using Microsoft.EntityFrameworkCore;
using MlmService.Data;
using MlmService.Dto.User;
using MlmService.Services.Interface;

namespace MlmService.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> GetUserInfo(Guid userId)
    {
        var userDto = await _context.Users
            .Select(e => new UserDto
            {
                Id = e.Id,
                Username = e.Username,
            }).FirstOrDefaultAsync(e => e.Id == userId);

        return userDto;
    }
}