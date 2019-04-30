using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.ViewModels.Orders
{
    public class OrderViewModel
    {
        [Required]
        [Display(Name = "Event")]
        public string EventName { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public string CustomerName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Ordered On")]
        public DateTime OrderedOn { get; set; }
    }
}
