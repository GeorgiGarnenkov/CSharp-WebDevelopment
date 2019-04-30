using System.Linq;
using Chushka.ViewModels.Orders;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace Chushka.Controllers
{
    public class OrdersController : BaseController
    {
        [Authorize("Admin")]
        public IHttpResponse All()
        {
            var orders = this.Db.Orders.Select(x =>
                new OrderViewModel
                {
                    Id = x.Id,
                    Username = x.User.Username,
                    ProductName = x.Product.Name,
                    OrderedOn = x.OrderedOn,
                }).ToList();

            var model = new AllOrdersViewModel { Orders = orders };
            return this.View(model);
        }
    }
}