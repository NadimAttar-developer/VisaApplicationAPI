

using Microsoft.AspNetCore.Identity;

namespace VisaApplication.Model.Security;
public class VisaApplicationUserRole : IdentityUserRole<Guid>
{
    #region Base Entity
    public bool IsValid { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public DateTimeOffset LastUpdatedDate { get; set; }
    public Guid CreationByGuid { get; set; }
    public Guid LastUpdatedByGuid { get; set; }
    #endregion

    #region Navigation Properties
    public VisaApplicationUser User { get; set; }
    public VisaApplicationRole Role { get; set; }
    #endregion
}
