using System.Diagnostics;
using Eventures.Models;
using Microsoft.AspNetCore.Mvc;
using Eventures.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Eventures.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
