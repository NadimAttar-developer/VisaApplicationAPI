
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisaApplication.Model.Applicant;
using VisaApplication.Service.Repositories.Applicant.IData;
using VisaApplication.Service.Services.Applicant.Dto;
using VisaApplication.SqlServer.DataBase;
using VisaApplicationBase.DbContext;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Repositories.Applicant.Data;
public class ApplicantRepository : VisaApplicationRepository, IApplicantRepository
{
    #region Constructor
    public ApplicantRepository(
        VisaApplicationDbContext context) : base(context)
    {}
    #endregion

    public async Task<OperationResult<GenericOperationResult, bool>> ActionAsync(
        ApplicantFormDto applicantFormDto)
    {
        OperationResult<GenericOperationResult,
        bool> result = new(GenericOperationResult.Success);

        using var transactionAsync = await _context.Database.BeginTransactionAsync();

        try
        {
            var applicantEntity = await _context.Applicants
                .Where(applicant => applicant.IsValid && applicant.Id == applicantFormDto.Id)
                .FirstOrDefaultAsync();

            if (applicantEntity == null)
            {
                applicantEntity = new ApplicantSet
                {
                    Id = Guid.NewGuid(),
                    FirstName = applicantFormDto.FirstName,
                    LastName = applicantFormDto.LastName,
                    Age = applicantFormDto.Age,
                    Country = applicantFormDto.Country,
                    City = applicantFormDto.City,
                    VisaTypeId = applicantFormDto.VisaTypeId,
                    ApplicationStatus = applicantFormDto.ApplicationStatus,
                    CreatedBy = applicantFormDto.CreatedBy,
                };

                await _context.AddAsync(applicantEntity);
            }
            else
            {
                applicantEntity.FirstName = applicantFormDto.FirstName;
                applicantEntity.LastName = applicantFormDto.LastName;
                applicantEntity.Age = applicantFormDto.Age;
                applicantEntity.Country = applicantFormDto.Country;
                applicantEntity.City = applicantFormDto.City;
                applicantEntity.VisaTypeId = applicantFormDto.VisaTypeId;
                applicantEntity.ApplicationStatus = applicantFormDto.ApplicationStatus;

                _context.Update(applicantEntity);
            }

            await _context.SaveChangesAsync();

            await transactionAsync.CommitAsync();

            return result.UpdateResultData(true);
        }
        catch (Exception)
        {
            return result.AddError("Something went wrong")
              .UpdateStatusResult(GenericOperationResult.InternalServerError);
        }
    }

    public async Task<OperationResult<GenericOperationResult,
        IEnumerable<ApplicantInfoDto>>> GetApplicants()
    {
        var result = new OperationResult<GenericOperationResult,
            IEnumerable<ApplicantInfoDto>>()
            .UpdateStatusResult(GenericOperationResult.Success);

        try
        {
            var applicantDtos = await _context.Applicants
                .Include(applicant => applicant.VisaType)
                .Where(applicant => applicant.IsValid)
                .Select(applicant => new ApplicantInfoDto
                {
                    Id = applicant.Id,
                    FirstName = applicant.FirstName,
                    LastName = applicant.LastName,
                    Age = applicant.Age,
                    Country = applicant.Country,
                    City = applicant.City,
                    VisaType = applicant.VisaType.Name,
                    Active = applicant.ApplicationStatus == true ? "Active" : "Non Active"
                }).ToListAsync();

            return result.UpdateResultData(applicantDtos);
        }
        catch (Exception)
        {
            return result.AddError("Something went wrong")
                .UpdateStatusResult (GenericOperationResult.InternalServerError);
        }
    }

    public async Task<OperationResult<GenericOperationResult, ApplicantDetailsDto>> GetApplicantDetails(Guid id)
    {
        var result = new OperationResult<GenericOperationResult, 
            ApplicantDetailsDto>(GenericOperationResult.Success);

        try
        {
            var applicantDetailsDto = await _context.Applicants
                .Where(applicant => applicant.IsValid && applicant.Id == id)
                .Select(applicant => new ApplicantDetailsDto
                {
                    Id = applicant.Id,
                    FirstName = applicant.FirstName,
                    LastName = applicant.LastName,
                    Age = applicant.Age,
                    Country = applicant.Country,
                    City = applicant.City,
                    VisaTypeId = applicant.VisaTypeId,
                    ApplicationStatus = applicant.ApplicationStatus
                }).FirstOrDefaultAsync();

            return result.UpdateResultData(applicantDetailsDto);
        }
        catch (Exception)
        {
            return result.AddError("Something went wrong")
                .UpdateStatusResult(GenericOperationResult.InternalServerError);
        }
    }

    public async Task<Guid> FetchApplicantIdByUserID(Guid userId)
    {
        return await _context.Applicants
            .Where(applicant => applicant.IsValid && applicant.CreatedBy == userId)
            .Select(applicant => applicant.Id)
            .FirstOrDefaultAsync();
    }
}
