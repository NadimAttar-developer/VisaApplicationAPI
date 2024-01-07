
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisaApplication.Service.Repositories.VisaType.IData;
using VisaApplication.Service.Services.VisaType.Dto;
using VisaApplication.SqlServer.DataBase;
using VisaApplicationBase.DbContext;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Repositories.VisaType.Data;
public class VisaTypeRepository : VisaApplicationRepository, IVisaTypeRepository
{
    #region Constructor
    public VisaTypeRepository(
        VisaApplicationDbContext context) : base(context)
    { }
    #endregion
    public async Task<OperationResult<GenericOperationResult, 
        IEnumerable<VisaTypeDto>>> GetAvailableVisaTypesAsync()
    {
        OperationResult<GenericOperationResult,
            IEnumerable<VisaTypeDto>> result = new(GenericOperationResult.Success);

        try
        {
            var visaTypeResult = await _context.VisaTypes
                .Where(visaType => visaType.IsValid)
                .Select(visaType => new VisaTypeDto
                {
                    Id = visaType.Id,
                    Name = visaType.Name
                }).ToListAsync();

            return result.UpdateResultData(visaTypeResult);
        }
        catch (Exception ex)
        {
            return result.AddError("Something went wrong")
               .UpdateStatusResult(GenericOperationResult.InternalServerError);
        }
    }
}
