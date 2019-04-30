using System.Collections.Generic;

namespace MishMash.Models
{
    public class User
    {
        public User()
        {
            this.Channels = new HashSet<UserChannel>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<UserChannel> Channels { get; set; }

        public Role Role { get; set; }

        //•	Has an Id – a UUID String or an Integer.
        //•	Has an Username
        //•	Has a Password
        //•	Has an Email
        //•	Has Followed Channels – a collection of Channels.
        //•	Has an Role – can be one of the following values (“User”, “Admin”)

    }
}