
using Microsoft.AspNetCore.Identity;
using VisaApplication.Model.Security;
using VisaApplication.Seeder.IdentitySeed.IData;

namespace VisaApplication.Seeder.IdentitySeed.Data;
public class UserCreationService : IUserCreationService
{
    #region Properties And Constructors

    private readonly UserManager<VisaApplicationUser> _userManager;

    public UserCreationService(UserManager<VisaApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    #endregion
    public async Task CreateUsersAsync()
    {
        var user = new VisaApplicationUser
        {
            UserName = "nadim",
            IsValid = true,
            CreationDate = DateTimeOffset.UtcNow,
            LastUpdatedDate = DateTimeOffset.UtcNow
        };

        var result = await _userManager.CreateAsync(user, "Aaa@1234");

        if (result.Succeeded)
        {
            result = await _userManager.AddToRoleAsync(user, "Registered");
        }
    }
}
