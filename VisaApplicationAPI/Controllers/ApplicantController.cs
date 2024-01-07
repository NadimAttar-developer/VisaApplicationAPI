using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VisaApplication.Service.Repositories.Content.Data;
using VisaApplication.Service.Repositories.Content.IData;
using VisaApplication.Service.Services.Applicant.Dto;
using VisaApplication.Service.Services.Applicant.IData;
using VisaApplication.Service.Services.Content.Dto;
using VisaApplication.Service.Services.Content.IData;
using VisaApplicationAPI.Util.Constant;
using VisaApplicationAPI.Util.ExceptionHandler;

namespace VisaApplicationAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ApplicantController : ControllerBase
{
    #region Constructor && Properties
    private readonly IApplicantService _applicantService;
    private readonly IFileService _fileService;

    public ApplicantController(
        IApplicantService applicantService,
        IFileService fileService)
    {
        _applicantService = applicantService;
        _fileService = fileService;
    }
    #endregion

    [HttpPost]
    [Authorize]
    [Route( "[action]")]
    public async Task<IActionResult> Action(ApplicantFormDto applicantFormDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var result = await _applicantService.ActionAsync(
            applicantFormDto, Guid.Parse(userId));

        return result.EnumResult.GenericThrowExceptionError(
            result.ErrorMessages, result.Result);
    }
    [HttpGet]
    [Route(template: "[action]")]
    public async Task<IActionResult> GetApplicants()
    {
        var result = await _applicantService.GetApplicants();

        return result.EnumResult.GenericThrowExceptionError(result.ErrorMessages, result.Result);
    }

    [HttpGet]
    [Route(template: "[action]")]
    public async Task<IActionResult> GetApplicantDetails(string id)
    {
        var result = await _applicantService.GetApplicantDetails(id);

        return result.EnumResult.GenericThrowExceptionError(result.ErrorMessages, result.Result);
    }

    [HttpPost]
    [Authorize]
    [Route(template: "[action]")]
    public async Task<IActionResult> UploadDocuments(
        [FromForm] List<IFormFile> documents)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var applicantId = await _applicantService.FetchApplicantIdByUserID(Guid.Parse(userId));
        List<FileFormDto> filesDto = new();

        foreach (var document in documents)
        {
            var documentFile = await _fileService.SaveFile(
                DataFilesPathes.DocumentFile, document);

            var documentFileDto = documentFile.Result;

            filesDto.Add(new FileFormDto
            {
                Name = documentFileDto.Name,
                Extension = documentFileDto.Extension,
                Url = documentFileDto.Url,
            });
        }

        var result = await _fileService.AddCollectionFileAsync(
                filesDto, applicantId);

        return result.EnumResult.GenericThrowExceptionError(result.ErrorMessages, result.Result);
    }
}
