using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.ViewModels.Events
{
    public class MyEventViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [MinLength(length: 10, ErrorMessage = "Name should be at least 10 symbols long.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Start")]
        [DataType(DataType.DateTime)]
        public DateTime Start { get; set; } = DateTime.UtcNow;

        [Required]
        [Display(Name = "End")]
        [DataType(DataType.DateTime)]
        public DateTime End { get; set; } = DateTime.UtcNow.AddDays(1);

        [Required]
        [Display(Name = "Tickets")]
        [Range(0, int.MaxValue)]
        public int Tickets { get; set; }
    }
}
