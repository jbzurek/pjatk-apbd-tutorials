using tutorial11.DTOs;
using tutorial11.Helpers;

namespace tutorial11.Repositories.Interfaces;

public interface IAccountRepository
{
    public Task<DbAnswer> RegisterAsync(UserDto userDto);
}