using tutorial11.DTOs;
using tutorial11.Helpers;
using tutorial11.Repositories.Interfaces;

namespace tutorial11.Repositories.Implementations;

public class AccountRepository : IAccountRepository
{
    private readonly Context _context;

    public AccountRepository(Context context)
    {
        _context = context;
    }

    public Task<DbAnswer> RegisterAsync(UserDto userDto)
    {
        if (userDto.Password.Length < 6)
        {
            return DbAnswer.PasswordLengthIsNotProper;
        }

        var checkUser = await _context.Users.FirstOrDefaultAsync(e => e.Login == userDto.Login);

        if (checkUser != null)
        {
            return DbAnswer.UserIsAlreadyRegistered;
        }
    }
}