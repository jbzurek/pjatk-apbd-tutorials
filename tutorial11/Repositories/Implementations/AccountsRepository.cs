using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using tutorial11.Helpers;
using tutorial11.Models;
using tutorial11.Repositories.Interfaces;
using tutorial11.Models.DTOs;

namespace tutorial11.Repositories.Implementations;

public class AccountsRepository : IAccountsRepository
{
    private readonly Context _context;
    private readonly IConfiguration _configuration;

    public AccountsRepository(Context context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginHelper> LoginAsync(UserDto userDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(e => e.Login == userDto.Login);
        if (user == null)
        {
            return new LoginHelper(DbAnswer.UserNotFound);
        }

        if (user.Password != SecurityHelper.GetHashedPassword(userDto.Password, user.Salt))
        {
            return new LoginHelper(DbAnswer.BadPassword);
        }

        var token = GetToken();

        user.RefreshToken = Guid.NewGuid().ToString();
        user.RefreshTokenExpiration = DateTime.Now.AddHours(12);

        await _context.SaveChangesAsync();

        return new LoginHelper(DbAnswer.Success, new JwtSecurityTokenHandler().WriteToken(token), user.RefreshToken);
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

        var hashedPwdAndSalt = SecurityHelper.GetHashedPasswordAndSalt(userDto.Password);
        var user = new User
        {
            Login = userDto.Login,
            Password = hashedPwdAndSalt.Item1,
            Salt = hashedPwdAndSalt.Item2,
            RefreshToken = Guid.NewGuid().ToString(),
            RefreshTokenExpiration = DateTime.Now.AddHours(12)
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return DbAnswer.Success;
    }

    public async Task<LoginHelper> UpdateAccessTokenAsync(RefreshTokenDto tokenDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(e => e.RefreshToken == tokenDto.RefreshToken);
        if (user == null)
        {
            return new LoginHelper(DbAnswer.UserNotFound);
        }

        if (user.RefreshTokenExpiration < DateTime.Now)
        {
            return new LoginHelper(DbAnswer.RefreshTokenIsExpired);
        }

        var token = GetToken();

        user.RefreshToken = Guid.NewGuid().ToString();
        user.RefreshTokenExpiration = DateTime.Now.AddHours(12);

        await _context.SaveChangesAsync();

        return new LoginHelper(DbAnswer.Success, new JwtSecurityTokenHandler().WriteToken(token), user.RefreshToken);
    }

    private JwtSecurityToken GetToken()
    {
        Claim[] userClaims = new[]
        {
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "client")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost",
            audience: "http://localhost",
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials
        );

        return token;
    }
}