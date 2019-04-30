using System;
using System.Linq;
using Chushka.Data;
using Chushka.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chushka.Controllers
{
    public class OrdersController : BaseController
    {
        [Authorize(Roles = "Admin")]
        public IActionResult All()
        {
            var orders = this.Db.Orders.ToList();

            return this.View(orders);
        }

        [Authorize]
        public IActionResult Create(int id)
        {
            var username = this.User.Identity.Name;

            var user = this.Db
                .Users
                .FirstOrDefault(x => x.UserName == username);

            var product = this.Db
                .Products
                .FirstOrDefault(x => x.Id == id);

            if (user == null || product == null)
            {
                return this.RedirectToAction("Index", "Home");
            }

            var order = new Order()
            {
                ClientId = user.Id,
                Client = user,
                Product = product,
                ProductId = id,
                OrderedOn = DateTime.UtcNow
            };

            this.Db
                .Orders
                .Add(order);

            this.Db
                .SaveChanges();

            if (this.User.IsInRole("Admin"))
            {
                return this.RedirectToAction("All", "Orders");
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}
