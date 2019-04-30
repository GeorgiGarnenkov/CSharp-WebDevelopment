using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.ViewModels.Orders
{
    public class CreateOrderViewModel
    {
        public CreateOrderViewModel()
        {
            this.OrderedOn = DateTime.UtcNow;
        }

        [DataType(DataType.DateTime)]
        [Display(Name = "Ordered On")]
        public DateTime OrderedOn { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string EventId { get; set; }

        [Required]
        [Display(Name = "Tickets")]
        [Range(minimum: 1, maximum: int.MaxValue)]
        public int TicketsCount { get; set; }
    }
}
