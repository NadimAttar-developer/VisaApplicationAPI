
using System.ComponentModel.DataAnnotations.Schema;
using VisaApplication.Model.Base;

namespace VisaApplication.Model.Content;

[Table("Files", Schema = "Content")]
public abstract class FileSet : BaseEntity
{
    public string Name { get; set; }
    public string FileName { get; }
    public string Extension { get; set; }
    public string Url { get; set; }
    public abstract string SourceName { get; }
}
