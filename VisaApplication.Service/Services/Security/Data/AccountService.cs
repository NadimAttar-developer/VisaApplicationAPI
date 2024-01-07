
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisaApplication.Service.Repositories.Security.IData;
using VisaApplication.Service.Services.Security.Dto;
using VisaApplication.Service.Services.Security.IData;
using VisaApplication.SqlServer.DataBase;
using VisaApplicationBase.DbContext;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Services.Security.Data;
public class AccountService : VisaApplicationRepository, IAccountService
{
    #region Properties && Constructor
    private readonly IAccountRepository _accountRepository;
    public AccountService(
       VisaApplicationDbContext context,
       IAccountRepository accountRepository) : base(context)
    {
        _accountRepository = accountRepository;
    }
    #endregion



    public async Task<OperationResult<GenericOperationResult, bool>> CreateUser(
        RegisterDto registerDto)
    {
        var result = new OperationResult<GenericOperationResult, bool>(
            GenericOperationResult.Failed);

        try
        {
            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                result = result
                    .AddError("The password and confirm password should be same")
                    .UpdateStatusResult(GenericOperationResult.ValidationError);

                return result;
            }

            result = await _accountRepository.CreateUserAsync(registerDto);
            return result;
        }
        catch (Exception)
        {
            result.AddError("Something went wrong")
                  .UpdateStatusResult(GenericOperationResult.InternalServerError);
            return result;
        }
    }

    public async Task<OperationResult<GenericOperationResult, string>> LoginUser(
        LoginDto loginDto)
    {
        OperationResult<GenericOperationResult,
           string> result = new(GenericOperationResult.Success);

        try
        {
            var data = await _accountRepository.CheckIfTheUserHasCorrectPasswordAndUserNameAsync(
                loginDto.Password, loginDto.UserName);

            if (!data.Result)
            {
                return result.AddError(data.ErrorMessages)
                    .UpdateStatusResult(GenericOperationResult.ValidationError);
            }

            return await _accountRepository.LoginUserAsync(loginDto);
        }
        catch (Exception)
        {
            return result.AddError("Something went wrong")
                .UpdateStatusResult(GenericOperationResult.InternalServerError);
        }
    }

    public async Task<OperationResult<GenericOperationResult,
        IEnumerable<UserDto>>> GetUsers()
    {
        return await _accountRepository.GetUsers();
    }

}
