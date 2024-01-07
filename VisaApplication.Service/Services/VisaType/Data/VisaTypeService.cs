
using System.Collections.Generic;
using System.Threading.Tasks;
using VisaApplication.Service.Repositories.VisaType.IData;
using VisaApplication.Service.Services.VisaType.Dto;
using VisaApplication.Service.Services.VisaType.IData;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Services.VisaType.Data;
public class VisaTypeService : IVisaTypeService
{
    #region Properties && Constructor
    private readonly IVisaTypeRepository _visaTypeRepository;

    public VisaTypeService(IVisaTypeRepository visaTypeRepository)
    {
        _visaTypeRepository = visaTypeRepository;
    }

    #endregion

    public async Task<OperationResult<GenericOperationResult,
        IEnumerable<VisaTypeDto>>> GetAvailableVisaTypesAsync()
    {
        return await _visaTypeRepository.GetAvailableVisaTypesAsync();
    }
}
