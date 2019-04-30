using System;

namespace Chushka.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string ClientId { get; set; }
        public virtual User Client { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
