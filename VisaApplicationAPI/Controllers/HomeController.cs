using Microsoft.AspNetCore.Mvc;

namespace VisaApplicationAPI.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
