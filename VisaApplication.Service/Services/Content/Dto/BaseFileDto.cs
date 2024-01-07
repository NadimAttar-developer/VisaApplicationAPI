
namespace VisaApplication.Service.Services.Content.Dto;
public class BaseFileDto : IFileDto
{
    public string Name { get; set; }
    public string Extension { get; set; }
    public string Url { get; set; }
}
