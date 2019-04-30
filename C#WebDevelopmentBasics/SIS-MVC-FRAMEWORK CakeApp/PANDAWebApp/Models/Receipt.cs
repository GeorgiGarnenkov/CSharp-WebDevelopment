using System;

namespace PANDAWebApp.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public int RecipientId { get; set; }
        public virtual User Recipient { get; set; }

        public int PackageId { get; set; }
        public virtual Package Package { get; set; }

        //•	Has an Id – a GUID String or an Integer.
        //•	Has a Fee – a decimal number.
        //•	Has an Issued On – a DateTime object.
        //•	Has a Recipient – a User object.
        //•	Has a Package – a Package object.

    }
}