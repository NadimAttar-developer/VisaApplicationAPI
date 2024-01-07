
using System;

namespace VisaApplication.Service.Services.Applicant.Dto;
public class ApplicantDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
}
