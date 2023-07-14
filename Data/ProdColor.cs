using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoutiqueProje.Data
{
    public class ProdColor : BaseEntity
    {

        public int ColorCode { get; set; }
        public string Color { get; set; }
        [NotMapped]
        public string DisplayName => ColorCode + " - " + Color;
        [NotMapped]
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}

