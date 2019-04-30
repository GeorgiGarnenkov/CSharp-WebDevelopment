using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Eventures.Models
{
    // Add profile data for application users by adding properties to the EventuresUser class
    public class EventuresUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UCN { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        //• First Name – a string.
        //•	Last Name – a string.
        //•	Unique Citizen Number(UCN) – a string.
    }
}
