
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisaApplication.Model.Security;
using VisaApplication.Service.Repositories.Security.IData;
using VisaApplication.Service.Services.Security.Dto;
using VisaApplication.SqlServer.DataBase;
using VisaApplicationBase.DbContext;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Repositories.Security.Data;
public class AccountRepository : VisaApplicationRepository, IAccountRepository
{
    #region Properties And Constructors
    private readonly SignInManager<VisaApplicationUser> _signInManager;
    private readonly UserManager<VisaApplicationUser> _userManager; 

    public AccountRepository(
        VisaApplicationDbContext context,
        UserManager<VisaApplicationUser> userManager,
        SignInManager<VisaApplicationUser> signInManager): base(context, userManager)
    {
            _signInManager = signInManager;
            _userManager = userManager;
    }

    #endregion

    public async Task<OperationResult<GenericOperationResult, bool>> CreateUserAsync(
        RegisterDto registerDto)
    {
        var result = new OperationResult<GenericOperationResult, bool>(
            GenericOperationResult.Success);

        try
        {
            if (!await ExistUserAsync(registerDto.UserName))
            {
                var entity = CreateUserEntity(registerDto);

                var userToAdd = await _userManager.CreateAsync(entity, registerDto.Password);

                if (!userToAdd.Succeeded)
                {
                    result.AddError(string.Join(" ,", userToAdd.Errors.Select(e => e.Description).ToList()))
                        .UpdateStatusResult(GenericOperationResult.ValidationError);

                    return result;
                }

                await _userManager.AddToRoleAsync(entity, "Registered");
            }

            return new OperationResult<GenericOperationResult, bool>(GenericOperationResult.Success, true);
        }
        catch (Exception)
        {
            result.AddError("Something went wrong")
                .UpdateStatusResult(GenericOperationResult.InternalServerError);

            return result;
        }
    }

    public async Task<bool> ExistUserAsync(string userName) =>
        await _context.Users.AnyAsync(e => e.UserName == userName);

    public async Task<OperationResult<GenericOperationResult, string>> LoginUserAsync(
        LoginDto loginDto)
    {
        OperationResult<GenericOperationResult,
             string> result = new(GenericOperationResult.Success);

        try
        {
            var userEntity = _context.Users
                .Where(e => e.UserName == loginDto.UserName)
                .SingleOrDefault();

            await _signInManager.SignInAsync(userEntity, true);

            var token = await GenerateJwtTokenAsync(userEntity);

            return result.UpdateResultData(token);
        }
        catch (Exception)
        {
            result.AddError("Something went wrong")
                            .UpdateStatusResult(GenericOperationResult.InternalServerError);

            return result;
        }
    }

    private VisaApplicationUser CreateUserEntity(RegisterDto registerDto)
    {
        return new VisaApplicationUser
        {
            UserName = registerDto.UserName,
            CreationDate = DateTimeOffset.UtcNow,
            LastUpdatedDate = DateTimeOffset.UtcNow,
            PhoneNumberConfirmed = true,
            EmailConfirmed = true,
            IsValid = true,
        };
    }

    public async Task<OperationResult<GenericOperationResult, bool>> 
        CheckIfTheUserHasCorrectPasswordAndUserNameAsync(string password, string userName)
    {
        OperationResult<GenericOperationResult,
           bool> result = new(GenericOperationResult.Success);

        var userEntity = await GetUserByUserNameAsync(userName);

        try
        {
            if (userEntity == null || !await _userManager.CheckPasswordAsync(userEntity, password))
            {
                return result.AddError("There is an error in the Password or UserName")
                    .UpdateStatusResult(GenericOperationResult.ValidationError)
                    .UpdateResultData(false);
            }

            return result.UpdateResultData(true);
        }
        catch (Exception)
        {
            return result.UpdateResultData(false);
        }
    }

    private async Task<VisaApplicationUser> GetUserByUserNameAsync(
     string userName)
    {
        try
        {
            return await _userManager.FindByNameAsync(userName);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<OperationResult<GenericOperationResult, 
        IEnumerable<UserDto>>> GetUsers()
    {
        var result = new OperationResult<GenericOperationResult,
            IEnumerable<UserDto>>(GenericOperationResult.Success);

        try
        {
            var userDtos = await _context.Users
                .Where(user => user.IsValid)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName
                }).ToListAsync();

            return result.UpdateResultData(userDtos);
        }
        catch (Exception)
        {
            return result.AddError("Something went wrong")
                .UpdateStatusResult(GenericOperationResult.InternalServerError);
        }
    }
}
