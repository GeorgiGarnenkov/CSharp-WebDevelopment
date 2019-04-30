using System.Linq;
using Chushka.ViewModels.Home;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace Chushka.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            if (this.User.IsLoggedIn)
            {
                var products = this.Db
                    .Products.Select(x => new ProductViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price
                        
                    }).ToList();


                var model = new IndexViewModel
                {
                    Products = products,
                };


                return this.View("Home/IndexLoggedIn", model);
            }

            return this.View();
        }
    }
}