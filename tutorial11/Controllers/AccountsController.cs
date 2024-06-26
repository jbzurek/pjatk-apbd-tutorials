using Microsoft.AspNetCore.Mvc;
using tutorial11.Helpers;
using tutorial11.Models.DTOs;
using tutorial11.Repositories.Interfaces;

namespace tutorial11.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{   
    private readonly IConfiguration _configuration;
    private readonly IAccountRepository _accountRepository;
    private readonly IJwtAuthManager _jwtAuthManager;

    public AccountsController(IAccountRepository accountRepository, IConfiguration configuration, IJwtAuthManager jwtAuthManager)
    {
        _accountRepository = accountRepository;
        _configuration = configuration;
        _jwtAuthManager = jwtAuthManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        var (hashedPassword, salt) = SecurityHelper.GetHashedPasswordAndSalt(userDto.Password);

        var result = await _accountRepository.RegisterAsync(new UserDto
        {
            Login = userDto.Login,
            Password = hashedPassword,
            Salt = salt
        });

        if (result == DbAnswer.UserIsAlreadyRegistered)
        {
            return Conflict("User is already registered.");
        }
        else if (result == DbAnswer.PasswordLengthIsNotProper)
        {
            return BadRequest("Password length is not proper.");
        }
        else
        {
            return Ok("Registration successful.");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto userDto)
    {
        var user = await _accountRepository.FindByLoginAsync(userDto.Login);
        if (user == null)
        {
            return Unauthorized();
        }

        var hashedPassword = SecurityHelper.GetHashedPassword(userDto.Password, user.Salt);
        if (user.Password != hashedPassword)
        {
            return Unauthorized();
        }

        var jwtResult = _jwtAuthManager.GenerateTokens(user.IdUser.ToString());
        user.RefreshToken = jwtResult.RefreshToken;
        user.RefreshTokenExpiration = jwtResult.RefreshTokenExpiration;

        await _accountRepository.UpdateUserAsync(user);

        return Ok(new
        {
            AccessToken = jwtResult.AccessToken,
            RefreshToken = jwtResult.RefreshToken
        });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var user = await _accountRepository.FindByRefreshTokenAsync(refreshToken);
        if (user == null || user.RefreshTokenExpiration <= DateTime.UtcNow)
        {
            return Unauthorized();
        }

        var jwtResult = _jwtAuthManager.GenerateTokens(user.IdUser.ToString());
        user.RefreshToken = jwtResult.RefreshToken;
        user.RefreshTokenExpiration = jwtResult.RefreshTokenExpiration;

        await _accountRepository.UpdateUserAsync(user);

        return Ok(new
        {
            AccessToken = jwtResult.AccessToken,
            RefreshToken = jwtResult.RefreshToken
        });
    }
}