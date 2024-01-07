using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisaApplication.Model.Base;

namespace VisaApplication.Model.Applicant;
[Table("Applicants", Schema = "VisaApplication")]
public class ApplicantSet : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public bool ApplicationStatus { get; set; }

    #region Navigation Properties
    public VisaTypeSet VisaType { get; set; }
    public Guid VisaTypeId { get; set; }
    #endregion
}
