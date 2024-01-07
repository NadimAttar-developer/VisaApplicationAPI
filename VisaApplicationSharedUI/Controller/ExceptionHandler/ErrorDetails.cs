
using Newtonsoft.Json;

namespace VisaApplicationSharedUI.Controller.ExceptionHandler;
public class ErrorDetails
{
    public int statusCode { get; set; }
    public string errorMessage { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
