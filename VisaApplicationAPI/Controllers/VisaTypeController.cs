using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VisaApplication.Service.Services.VisaType.IData;
using VisaApplicationAPI.Util.ExceptionHandler;

namespace VisaApplicationAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VisaTypeController : ControllerBase
{
    #region Properties && Constructor
    private readonly IVisaTypeService _visaTypeService;

    public VisaTypeController(IVisaTypeService visaTypeService)
    {
        _visaTypeService = visaTypeService;
    }
    #endregion

    [HttpGet]
    [Route(template: "[action]")]
    public async Task<IActionResult> GetAvailableVisaApplication()
    {
        var result = await _visaTypeService.GetAvailableVisaTypesAsync();

        return result.EnumResult.GenericThrowExceptionError(
            result.ErrorMessages, result.Result);
    }
}
