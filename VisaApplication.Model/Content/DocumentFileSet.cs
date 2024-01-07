
using System.ComponentModel.DataAnnotations.Schema;
using VisaApplication.Model.Applicant;

namespace VisaApplication.Model.Content;
[Table("DocumentFiles", Schema = "Content")]
public class DocumentFileSet : FileSet
{
    public override string SourceName => "";
    public ApplicantSet Applicant { get; set; }
    public Guid ApplicantId { get; set; }
}
