using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Chushka.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Orders = new HashSet<Order>();
        }

        public string FullName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
