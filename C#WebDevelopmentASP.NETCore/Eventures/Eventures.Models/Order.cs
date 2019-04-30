using System;

namespace Eventures.Models
{
    public class Order
    {
        public string Id { get; set; }

        public DateTime OrderedOn { get; set; }

        public string CustomerId { get; set; }
        public virtual EventuresUser Customer { get; set; }

        public string EventId { get; set; }
        public virtual Event Event { get; set; }

        public int TicketsCount { get; set; }
    }
}