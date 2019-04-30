using System;
using System.Collections.Generic;
using System.Text;
using IRunes.Data.Models;

namespace IRunes.Data.Models
{
    public class User
    {
        public User()
        {
            this.Albums = new HashSet<Album>();
        }
        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}