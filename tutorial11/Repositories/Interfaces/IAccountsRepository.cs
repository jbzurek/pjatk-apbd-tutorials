using tutorial11.Helpers;
using tutorial11.Models;
using tutorial11.Models.DTOs;

namespace tutorial11.Repositories.Interfaces;

public interface IAccountsRepository
{
    Task<DbAnswer> RegisterAsync(UserDto userDto);
    Task<User> FindByLoginAsync(string login);
    Task<User> FindByRefreshTokenAsync(string refreshToken);
    Task UpdateUserAsync(User user);
}