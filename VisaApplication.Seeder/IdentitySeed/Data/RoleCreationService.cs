
using Microsoft.AspNetCore.Identity;
using VisaApplication.Model.Security;
using VisaApplication.Seeder.IdentitySeed.IData;

namespace VisaApplication.Seeder.IdentitySeed.Data;
public class RoleCreationService : IRoleCreationService
{
    #region Properties And Constructors

    private readonly RoleManager<VisaApplicationRole> _roleManager;

    public RoleCreationService(RoleManager<VisaApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }
    #endregion
    public async Task CreateRolesAsync()
    {
        var role = await _roleManager.FindByNameAsync("Registered");

        if (role is null)
        {
            await _roleManager.CreateAsync(new VisaApplicationRole
            {
                Name = "Registered",
                IsValid = true,
                CreationDate = DateTimeOffset.UtcNow,
                LastUpdatedDate = DateTimeOffset.UtcNow
            });
        }
    }
}
