using System.Diagnostics;
using System.Linq;
using Chushka.Data;
using Microsoft.AspNetCore.Mvc;
using Chushka.ViewModels;

namespace Chushka.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var products = this.Db
                    .Products
                    .Select(x => new ShowProductViewModel(x.Id, x.Name, x.Price, x.Description, x.Type.ToString()))
                    .ToList();

                return this.View("IndexLoggedIn", products);
            }
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
