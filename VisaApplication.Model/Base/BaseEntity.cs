using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisaApplication.Model.Base;
public class BaseEntity
{
    public BaseEntity()
    {
        IsValid = true;
        CreationDate = DateTimeOffset.UtcNow;
        LastUpdatedDate = DateTimeOffset.UtcNow;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public bool IsValid { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset LastUpdatedDate { get; set; }
    public Guid? LastUpdatedBy { get; set; }
}
