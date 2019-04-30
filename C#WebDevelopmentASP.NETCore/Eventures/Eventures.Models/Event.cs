using System;
using System.Collections.Generic;

namespace Eventures.Models
{
    public class Event
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime Start { get; set; } = DateTime.UtcNow;

        public DateTime End { get; set; } = DateTime.UtcNow.AddDays(1);

        public int TotalTickets { get; set; }

        public decimal PricePerTicket { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();

        //•	Id – a GUID.
        //•	Name – a string.
        //•	Place – a string.
        //•	Start – a DateTime object.
        //•	End – a DateTime object.
        //•	Total Tickets – an integer.
        //•	Price Per Ticket – a decimal value.

    }
}