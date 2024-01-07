
using System.Collections.Generic;
using System.Threading.Tasks;
using VisaApplication.Service.Services.VisaType.Dto;
using VisaApplicationBase.OperationResult.Enum;
using VisaApplicationBase.OperationResult;

namespace VisaApplication.Service.Services.VisaType.IData;
public interface IVisaTypeService
{
    Task<OperationResult<GenericOperationResult,
       IEnumerable<VisaTypeDto>>> GetAvailableVisaTypesAsync();
}
