using tutorial11.Helpers;
using tutorial11.Models;
using tutorial11.Repositories.Interfaces;
using tutorial11.Models.DTOs;

namespace tutorial11.Repositories.Implementations;

public class AccountsesRepository : IAccountsRepository
{
    private readonly Context _context;

    public AccountsesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DbAnswer> RegisterAsync(UserDto userDto)
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

        var newUser = new User
        {
            Login = userDto.Login,
            Password = userDto.Password,
            Salt = userDto.Salt
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return DbAnswer.Success;
    }

    public async Task<User> FindByLoginAsync(string login)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Login == login);
    }

    public async Task<User> FindByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}