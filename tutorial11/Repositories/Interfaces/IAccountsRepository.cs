using tutorial11.Helpers;
using tutorial11.Models.DTOs;

namespace tutorial11.Repositories.Interfaces;

public interface IAccountsRepository
{
    Task<LoginHelper> LoginAsync(UserDto userDto);
    Task<DbAnswer> RegisterAsync(UserDto userDto);
    Task<LoginHelper> UpdateAccessTokenAsync(RefreshTokenDto tokenDto);
}