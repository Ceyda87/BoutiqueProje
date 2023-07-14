using BoutiqueProje.Controllers;
using BoutiqueProje.Data;

namespace BoutiqueProje.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; } 
        public int CategoryId { get; set; }
        public int Size { get; set; }
        public int ImageId { get; set; }
        public string ImageName { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();

    }
}

