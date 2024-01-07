
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using VisaApplicationBase.OperationResult.Enum;
using VisaApplicationBase.OperationResult;
using VisaApplication.Service.Services.Content.Dto;
using System.Collections.Generic;
using System;

namespace VisaApplication.Service.Services.Content.IData;
public interface IFileService
{
    Task<OperationResult<GenericOperationResult, IFileDto>> SaveFile(
        string path, IFormFile formFile);

    Task<OperationResult<GenericOperationResult, bool>> AddCollectionFileAsync(
      List<FileFormDto> dto, Guid objectId);
}
