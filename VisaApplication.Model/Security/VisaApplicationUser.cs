
using Microsoft.AspNetCore.Identity;

namespace VisaApplication.Model.Security;
public class VisaApplicationUser : IdentityUser<Guid>
{
    #region Base Entity

    public bool IsValid { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset LastUpdatedDate { get; set; }
    public Guid CreationByGuid { get; set; }
    public Guid LastUpdatedByGuid { get; set; }
    #endregion

    #region Navigation Properties
    public ICollection<VisaApplicationUserRole> UserRoles { get; set; }
    #endregion
}
