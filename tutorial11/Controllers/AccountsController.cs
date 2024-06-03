using Microsoft.AspNetCore.Mvc;
using tutorial11.DTOs;
using tutorial11.Repositories.Interfaces;

namespace tutorial11.Controllers;

public class AccountsController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountsController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        var result = await _accountRepository.RegisterAsync(userDto);
        return null;
        // TODO
    } 
}