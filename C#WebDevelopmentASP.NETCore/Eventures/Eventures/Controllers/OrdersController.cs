using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eventures.Data;
using Eventures.Models;
using Eventures.ViewModels;
using Eventures.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eventures.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EventuresDbContext db;
        private readonly IMapper mapper;

        public OrdersController(EventuresDbContext dbContext, IMapper mapper)
        {
            this.db = dbContext;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateOrderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentEvent = await this.db
                    .Events.FirstOrDefaultAsync(x => x.Id == viewModel.EventId);

                if (viewModel.TicketsCount > currentEvent.TotalTickets)
                {
                    ModelState.AddModelError(string.Empty, "Event does not have enough tickets!");
                    return RedirectToAction("All", "Events");
                }

                currentEvent.TotalTickets -= viewModel.TicketsCount;

                this.db.Events.Update(currentEvent);

                var order = this.mapper.Map<CreateOrderViewModel, Order>(viewModel);

                await this.db.Orders.AddAsync(order);
                await this.db.SaveChangesAsync();

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("All", "Orders");
                }

                return RedirectToAction("MyEvents", "Events");
            }

            ModelState.AddModelError(string.Empty, "Try placing your order again.");
            return RedirectToAction("All", "Events");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> All()
        {
            var orders = await this.db
                .Orders
                .ToListAsync();

            var allOrders = this.mapper
                .Map<List<Order>, IEnumerable<OrderViewModel>>(orders);

            allOrders = allOrders.OrderByDescending(x => x.OrderedOn);

            return View(allOrders);
        }
    }
}