
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisaApplication.Model.Security;
public class VisaApplicationRole : IdentityRole<Guid>
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
