using System;
using System.Collections.Generic;

namespace MishMash.Models
{
    public class Channel
    {
        public Channel()
        {
            this.Tags = new HashSet<ChannelTag>();
            this.Followers = new HashSet<UserChannel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Type Type { get; set; }

        public virtual ICollection<ChannelTag> Tags { get; set; }

        public virtual ICollection<UserChannel> Followers { get; set; }

        //•	Has an Id – a UUID String or an Integer.
        //•	Has a Name
        //•	Has a Description
        //•	Has a Type – can be one of the following values (“Game”, “Motivation”, “Lessons”, “Radio”, “Other”).
        //•	Has Tags – a collection of Strings.
        //•	Has Followers – a collection of Users.
    }
}