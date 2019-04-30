using System.Collections.Generic;
using Chushka.Models;

namespace Chushka.ViewModels.Products
{
    public class CreateProductInputModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }


    }
}