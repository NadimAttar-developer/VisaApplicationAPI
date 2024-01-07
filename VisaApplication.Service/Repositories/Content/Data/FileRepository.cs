
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisaApplication.Model.Content;
using VisaApplication.Service.Repositories.Content.IData;
using VisaApplication.Service.Services.Content.Dto;
using VisaApplication.SqlServer.DataBase;
using VisaApplicationBase.DbContext;
using VisaApplicationBase.OperationResult;
using VisaApplicationBase.OperationResult.Enum;

namespace VisaApplication.Service.Repositories.Content.Data;
public class FileRepository : VisaApplicationRepository, IFileRepository
{
    #region Constructor
    public FileRepository(
        VisaApplicationDbContext context) : base(context)
    {}
    #endregion
    public async Task<OperationResult<GenericOperationResult, bool>> AddCollectionFileAsync(
        List<FileFormDto> dto, Guid objectId)
    {
        OperationResult<GenericOperationResult,
             bool> result = new(GenericOperationResult.Failed);

        try
        {
            var entities = new List<FileSet>();
            foreach (var item in dto)
            {
                var file = CreateFile(objectId);

                file.Name = item.Name;
                file.Extension = item.Extension;
                file.Url = item.Url;

                entities.Add(file);
            }

            if (entities.Any())
            {
                await _context.Files.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }

            return result.UpdateResultData(true);
        }
        catch (Exception)
        {
            result.AddError("Something went wrong")
                 .UpdateStatusResult(GenericOperationResult.InternalServerError);
            return result;
        }
    }

    private FileSet CreateFile(Guid objectId)
    {
        return new DocumentFileSet
        {
            Id = Guid.NewGuid(),
            ApplicantId = objectId
        };
    }
}
