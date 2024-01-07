using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisaApplication.Service.Services.Security.Dto;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Services.Security.IData;
public interface IAccountService
{
    Task<OperationResult<GenericOperationResult, bool>> CreateUser(
        RegisterDto registerDto);

    Task<OperationResult<GenericOperationResult, string>> LoginUser(
        LoginDto loginDto);

    Task<OperationResult<GenericOperationResult, 
        IEnumerable<UserDto>>> GetUsers();
}
