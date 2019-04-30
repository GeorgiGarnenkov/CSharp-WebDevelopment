using Chushka.Models;

namespace Chushka.ViewModels.Products
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ProductType Type { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}