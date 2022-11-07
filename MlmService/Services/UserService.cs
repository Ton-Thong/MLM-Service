using Microsoft.EntityFrameworkCore;
using MlmService.Database;
using MlmService.Dto.User;
using MlmService.Services.Interface;

namespace MlmService.Services;

public class UserService : IUserService
{
    private readonly DatabaseContext _context;

    public UserService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<UserDto> GetUserInfo(Guid userId)
    {
        return await _context.Users
            .Select(e => new UserDto
            {
                Id = e.Id,
                Username = e.Username,
            }).FirstOrDefaultAsync(e => e.Id == userId);
    }
}