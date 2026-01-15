using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FitnessApp.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
