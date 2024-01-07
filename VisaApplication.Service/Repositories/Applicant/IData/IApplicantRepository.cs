
using System.Threading.Tasks;
using VisaApplication.Service.Services.Applicant.Dto;
using VisaApplicationBase.OperationResult.Enum;
using VisaApplicationBase.OperationResult;
using System.Collections;
using System.Collections.Generic;
using System;

namespace VisaApplication.Service.Repositories.Applicant.IData;
public interface IApplicantRepository
{
    Task<OperationResult<GenericOperationResult, bool>> ActionAsync(
        ApplicantFormDto applicantFormDto);

    Task<OperationResult<GenericOperationResult, 
        IEnumerable<ApplicantInfoDto>>> GetApplicants();

    Task<OperationResult<GenericOperationResult, ApplicantDetailsDto>> GetApplicantDetails(Guid id);

    Task<Guid> FetchApplicantIdByUserID(Guid userId);
}
