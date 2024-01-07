
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VisaApplication.Model.Security;
using VisaApplication.SqlServer.DataBase;

namespace VisaApplicationBase.DbContext;
public class VisaApplicationRepository
{
    #region Properties And Constructors

    protected readonly UserManager<VisaApplicationUser> _userManager;
    protected readonly VisaApplicationDbContext _context;

    public VisaApplicationRepository(
        VisaApplicationDbContext context,
        UserManager<VisaApplicationUser> userManager = null)
    {
        _userManager = userManager;
        _context = context;
    }
    #endregion

    #region Helper Methods
    protected async Task<string> GenerateJwtTokenAsync(VisaApplicationUser user)
    {
        var roleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
         
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("NADIMATTARPASSEDFROMHERE"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, roleName)
            };

        var token = new JwtSecurityToken(
            "https://localhost:44312/",
            "https://localhost:44312/",
            claims,
            expires: DateTime.Now.AddMonths(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    #endregion
}
