using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Threading.Tasks;
using VisaApplication.Service.Services.Security.Dto;
using VisaApplication.Service.Services.Security.IData;
using VisaApplicationAPI.Util.ExceptionHandler;

namespace VisaApplicationAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    #region Properties && Constructor
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    #endregion

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> CreateUser(RegisterDto registerDto)
    {
        var result = await _accountService.CreateUser(registerDto);

        return result.EnumResult.GenericThrowExceptionError(result.ErrorMessages, result.Result);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _accountService.LoginUser(loginDto);

        return result.EnumResult.GenericThrowExceptionError(result.ErrorMessages, result.Result);
    }
}
