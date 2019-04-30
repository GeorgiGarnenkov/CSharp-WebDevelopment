using System;
using System.Linq;
using Eventures.ViewModels;
using Eventures.ViewModels.Events;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Eventures.Filters
{
    public class EventsLogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger logger;
        private CreateEventViewModel model;

        public EventsLogActionFilter(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<EventsLogActionFilter>();

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.model = context.ActionArguments.Values.OfType<CreateEventViewModel>().Single();

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (this.model != null)
            {
                var username = context.HttpContext.User.Identity.Name;
                this.logger.LogInformation(
                    $"[{DateTime.UtcNow}] Administrator {username} created event {this.model.Name} ({this.model.Start} / {this.model.End}).");
            }

            base.OnActionExecuted(context);
        }
    }
}