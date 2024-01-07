
using Microsoft.Data.SqlClient.DataClassification;
using System;

namespace VisaApplication.Service.Services.Security.Dto;
public class UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
}
