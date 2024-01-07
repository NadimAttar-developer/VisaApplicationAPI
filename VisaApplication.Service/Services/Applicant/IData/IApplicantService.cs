
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisaApplication.Service.Services.Applicant.Dto;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Services.Applicant.IData;
public interface IApplicantService
{
    Task<OperationResult<GenericOperationResult, bool>> ActionAsync(
        ApplicantFormDto applicantFormDto, Guid userId);

    Task<OperationResult<GenericOperationResult,
        IEnumerable<ApplicantInfoDto>>> GetApplicants();

    Task<OperationResult<GenericOperationResult, ApplicantDetailsDto>> 
        GetApplicantDetails(string id);

    Task<Guid> FetchApplicantIdByUserID(Guid userId);
}
