
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VisaApplication.Service.Repositories.Content.IData;
using VisaApplication.Service.Services.Content.Dto;
using VisaApplication.Service.Services.Content.IData;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Services.Content.Data;

public class FileService : IFileService
{
    #region Properties && Constructor
    private IHostingEnvironment _hostingEnvironment;
    private readonly IFileRepository _fileRepository;

    public FileService(IFileRepository fileRepository,
        IHostingEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
        _fileRepository = fileRepository;
    }

    #endregion

    public async Task<OperationResult<GenericOperationResult, IFileDto>> SaveFile(
        string path, IFormFile formFile)
    {
        try
        {
            bool pathIsExist = Directory.Exists(Path.Combine(_hostingEnvironment.WebRootPath, path));
            if (!pathIsExist)
                Directory.CreateDirectory(Path.Combine(_hostingEnvironment.WebRootPath, path));
            Guid fileName = Guid.NewGuid();
            string fileExtension = Path.GetExtension(formFile.FileName);
            string physicalFileName = $"{fileName}{fileExtension}";
            string physicalFilePath = Path.Combine(_hostingEnvironment.WebRootPath, path, physicalFileName);
            using (FileStream fileStream = new FileStream(physicalFilePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream, new CancellationToken());
            }

            var fileDto = new BaseFileDto()
            {
                Name = formFile.Name,
                Extension = fileExtension,
                Url = Path.Combine(path, physicalFileName),
            };

            return new OperationResult<GenericOperationResult, IFileDto>(
                GenericOperationResult.Success, fileDto, null);
        }
        catch (Exception ex)
        {
            return new OperationResult<GenericOperationResult, IFileDto>(
                GenericOperationResult.Failed, null, ex);
        }
    }

    public async Task<OperationResult<GenericOperationResult, bool>> AddCollectionFileAsync(
      List<FileFormDto> dto, Guid objectId)
    {
        var result = new OperationResult<GenericOperationResult, bool>();

        try
        {
            if (dto.Any())
            {
                await _fileRepository.AddCollectionFileAsync(dto, objectId);
            }

            return result.UpdateResultData(true);
        }
        catch (Exception)
        {
            return result.AddError("Something went wrong")
                .UpdateStatusResult(GenericOperationResult.ValidationError)
                .UpdateResultData(false);
        }
    }
}
