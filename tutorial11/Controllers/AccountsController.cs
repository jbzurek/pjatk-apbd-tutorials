using Microsoft.AspNetCore.Mvc;
using tutorial11.Helpers;
using tutorial11.Models.DTOs;
using tutorial11.Repositories.Interfaces;

namespace tutorial11.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountsRepository _accountsRepository;

    public AccountsController(IAccountsRepository accountsRepository)
    {
        _accountsRepository = accountsRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(UserDto userDto)
    {
        var result = await _accountsRepository.RegisterAsync(userDto);

        switch (result)
        {
            case DbAnswer.Success:
                return Ok("Successfully registered!");
            case DbAnswer.PasswordLengthIsNotProper:
                return BadRequest("Password is too short!");
            case DbAnswer.UserIsAlreadyRegistered:
                return BadRequest("User with the same login is already registered!");
            default:
                return StatusCode(500);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(UserDto userDto)
    {
        var result = await _accountsRepository.LoginAsync(userDto);

        switch (result.DbAnswer) 
        {
            case DbAnswer.Success:
                return Ok(result);
            case DbAnswer.BadPassword:
                return Unauthorized("Password is wrong!");
            case DbAnswer.UserNotFound:
                return Unauthorized("Login is not found!");
            default:
                return Unauthorized();
        }
    }

    [HttpPost("updateAccessToken")]
    public async Task<IActionResult> UpdateAccessTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        var result = await _accountsRepository.UpdateAccessTokenAsync(refreshTokenDto);

        switch (result.DbAnswer)
        {
            case DbAnswer.Success:
                return Ok(result);
            case DbAnswer.RefreshTokenIsExpired:
                return BadRequest("Refresh token is expired!");
            case DbAnswer.UserNotFound:
                return BadRequest("User is not found!");
            default:
                return Unauthorized();
        }
    }
}