using System;

namespace VisaApplication.Service.Services.Applicant.Dto;
public class ApplicantFormDto : ApplicantDto
{
    public Guid VisaTypeId { get; set; }
    public bool ApplicationStatus { get; set; }
    public Guid CreatedBy { get; set; }
}
