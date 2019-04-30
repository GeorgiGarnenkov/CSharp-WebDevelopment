using System.ComponentModel.DataAnnotations;

namespace Chushka.ViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
