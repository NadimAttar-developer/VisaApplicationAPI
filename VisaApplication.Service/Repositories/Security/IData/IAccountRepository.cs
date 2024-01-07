using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisaApplication.Service.Services.Security.Dto;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Repositories.Security.IData;
public interface IAccountRepository
{
    Task<OperationResult<GenericOperationResult, bool>> CreateUserAsync(
        RegisterDto registerDto);

    Task<OperationResult<GenericOperationResult, string>> LoginUserAsync(
        LoginDto loginDto);

    Task<bool> ExistUserAsync(string userName);
    Task<OperationResult<GenericOperationResult, bool>> CheckIfTheUserHasCorrectPasswordAndUserNameAsync(
         string password, string userName);

    Task<OperationResult<GenericOperationResult, IEnumerable<UserDto>>> GetUsers();
}
