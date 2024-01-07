
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using VisaApplication.Service.Services.Content.Dto;
using VisaApplicationBase.OperationResult.Enum;
using VisaApplicationBase.OperationResult;

namespace VisaApplication.Service.Repositories.Content.IData;
public interface IFileRepository
{
    Task<OperationResult<GenericOperationResult, bool>> AddCollectionFileAsync(
      List<FileFormDto> dto, Guid objectId);
}
