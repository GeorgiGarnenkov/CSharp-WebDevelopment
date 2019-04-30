using System.ComponentModel.DataAnnotations;

namespace Chushka.ViewModels
{
    public class ShowProductViewModel
    {
        public ShowProductViewModel(int id, string name, decimal price, string description, string type)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Description = description;
            this.Type = type;

            this.FormatDescription();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }

        private void FormatDescription()
        {
            var substringLength = this.Description.Length >= 50 ? 50 : this.Description.Length;
            var suffix = substringLength == 50 ? "..." : string.Empty;
            this.Description = this.Description.Substring(0, substringLength) + suffix;
        }
    }
}
