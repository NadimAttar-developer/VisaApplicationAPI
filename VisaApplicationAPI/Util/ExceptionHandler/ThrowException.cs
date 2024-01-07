using Microsoft.AspNetCore.Mvc;
using VisaApplicationBase.OperationResult.Enum;
using VisaApplicationSharedUI.Controller.ExceptionHandler;

namespace VisaApplicationAPI.Util.ExceptionHandler;

public static class ThrowException
{
    public static JsonResult GenericThrowExceptionError(this GenericOperationResult enumResult, string errorMessage, object obj)
    {
        switch (enumResult)
        {
            case GenericOperationResult.Success:
                return new JsonResult(obj);
            case GenericOperationResult.Failed:
                throw new InternalServerException(errorMessage);
            case GenericOperationResult.NotFound:
                throw new NotFoundException(errorMessage);
            case GenericOperationResult.ValidationError:
                throw new BadRequestException(errorMessage);
            case GenericOperationResult.InternalServerError:
                throw new InternalServerException(errorMessage);
            default:
                throw new InternalServerException(errorMessage);
        }
    }
}