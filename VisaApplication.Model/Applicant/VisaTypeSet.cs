
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VisaApplication.Model.Base;

namespace VisaApplication.Model.Applicant;
[Table("VisaTypes", Schema = "VisaApplication")]
public class VisaTypeSet : BaseEntity
{
    public string Name { get; set; }
    #region Navigation Properties
    public ICollection<ApplicantSet> Applicants { get; set; }
    #endregion
}
