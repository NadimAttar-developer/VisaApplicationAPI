
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VisaApplication.Service.Services.VisaType.Dto;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Repositories.VisaType.IData;
public interface IVisaTypeRepository
{
    Task<OperationResult<GenericOperationResult, 
        IEnumerable<VisaTypeDto>>> GetAvailableVisaTypesAsync();
}
