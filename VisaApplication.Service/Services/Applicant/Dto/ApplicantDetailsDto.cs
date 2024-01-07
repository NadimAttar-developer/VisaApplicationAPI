
using System;

namespace VisaApplication.Service.Services.Applicant.Dto;
public class ApplicantDetailsDto : ApplicantDto
{
    public Guid VisaTypeId { get; set; }
    public bool ApplicationStatus { get; set; }
}
