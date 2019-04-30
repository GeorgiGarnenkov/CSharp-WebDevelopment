using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Eventures.Data;
using Eventures.Models;
using Eventures.Filters;
using Eventures.ViewModels.Events;
using Eventures.ViewModels.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eventures.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventuresDbContext db;
        private readonly IMapper mapper;
        private readonly ILogger<EventsController> logger;

        public EventsController(EventuresDbContext dbContext, IMapper mapper, ILogger<EventsController> logger)
        {
            this.db = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var events = await this.db
                .Events.Where(x => x.TotalTickets > 0)
                .ToListAsync();

            var allEvents = this.mapper
                .Map<List<Event>, IEnumerable<CreateEventViewModel>>(events);
            
            var viewModel = new CreateEventOrderViewModel()
            {
                CreateEventViewModels = allEvents,
                CreateOrderViewModel = new CreateOrderViewModel()
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ServiceFilter(typeof(EventsLogActionFilter))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateEventViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Preventing creating invalid Id for Event
                viewModel.Id = null;

                var createdEvent = this.mapper.Map<CreateEventViewModel, Event>(viewModel);

                await this.db.Events.AddAsync(createdEvent);
                await this.db.SaveChangesAsync();

                this.logger.LogInformation("Event created: ", createdEvent.Name, createdEvent);

                return RedirectToAction("All");
            }

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> MyEvents()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var currentUserEvents = await this.db
                .Users
                .Where(x => x.Id == userId)
                .SelectMany(x => x.Orders)
                .Select(x => x.Event)
                .Select(x => new MyEventViewModel()
                {
                    Name = x.Name,
                    Start = x.Start,
                    End = x.End,
                    Tickets = x.Orders
                               .Where(o => o.CustomerId == userId)
                               .Sum(t => t.TicketsCount)
                })
                .Distinct()
                .ToListAsync();

            return View(currentUserEvents);
        }
    }
}