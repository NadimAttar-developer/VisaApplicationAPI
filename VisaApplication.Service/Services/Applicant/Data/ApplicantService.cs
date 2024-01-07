
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisaApplication.Service.Repositories.Applicant.IData;
using VisaApplication.Service.Services.Applicant.Dto;
using VisaApplication.Service.Services.Applicant.IData;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Services.Applicant.Data;
public class ApplicantService : IApplicantService
{

    #region Properties && Constructor
    private readonly IApplicantRepository _applicantRepository;

    public ApplicantService(IApplicantRepository applicantRepository)
    {
        _applicantRepository = applicantRepository;
    }
    #endregion


    public async Task<OperationResult<GenericOperationResult, bool>> ActionAsync(
        ApplicantFormDto applicantFormDto, Guid userId)
    {
        if (applicantFormDto == null)
        {
            return new OperationResult<GenericOperationResult, bool>()
                .AddError("There is no Data Entered")
                .UpdateStatusResult(GenericOperationResult.ValidationError);
        }

        applicantFormDto.CreatedBy = userId;

        return await _applicantRepository.ActionAsync(applicantFormDto);
    }

    public async Task<OperationResult<GenericOperationResult, ApplicantDetailsDto>> GetApplicantDetails(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return new OperationResult<GenericOperationResult, ApplicantDetailsDto>(
                GenericOperationResult.NotFound)
                .AddError("There is no Id");
        }

        return await _applicantRepository.GetApplicantDetails(Guid.Parse(id));
    }

    public async Task<OperationResult<GenericOperationResult,
        IEnumerable<ApplicantInfoDto>>> GetApplicants()
    {
        return await _applicantRepository.GetApplicants();
    }

    public async Task<Guid> FetchApplicantIdByUserID(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return Guid.Empty;
        }

        return await _applicantRepository.FetchApplicantIdByUserID(userId);
    }
}
