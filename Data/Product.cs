using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Security.Policy;
using System.Text.Json.Serialization;

namespace BoutiqueProje.Data
{
    public class BaseEntity
    {
        public int Id { get; set; }        
        public string Name { get; set; } = "";        
        public string Description { get; set; } = "";     

        
    }       

    public class Product: BaseEntity
    {
        
        public string ProductName { get; set; }
        [Display(Name = "Ürün Adı")]
        public int ProductCode { get; set; }
        [NotMapped]
        public string DisplayName => ProductCode + " - " + ProductName;
        public string Color { get; set; }
        [Display(Name = "Renk")]
        public int ColorCode { get; set; }
        [Display(Name="Kategoriler")]
        public int CategoryId { get; set; }
        [Display(Name="Beden")]
        public int SizeId { get; set; }
        public int Size { get; set; }
        [NotMapped]
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public virtual Category Category { get; set; }
        public virtual Image Image { get; set; }
        public virtual ProdColor ProdColor { get; set; }       
        public virtual List<ProdColor> ProdColors { get; set; } = new List<ProdColor>();       


    }   

    public class Category : BaseEntity 
    {
        
        public virtual List<Product>Products { get; set; } = new List<Product>();
        

    }        

    public class BoutiqueProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProdColor> ProdColors { get; set; }
        

        public BoutiqueProductContext(DbContextOptions<BoutiqueProductContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProdColor>().Property(p => p.Color)
                .HasDefaultValueSql("ColorCode")
                .IsRequired();

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ProductName = "Gömlek", SizeId = 34, CategoryId = 1, ColorCode = 9 , ImageName= "1.jpg"},
                new Product { Id = 2, ProductName = "Pantalon", SizeId = 38, CategoryId = 2, ColorCode = 2 , ImageName = "2.jpg" },
                new Product { Id = 3, ProductName = "Etek", SizeId = 36, CategoryId = 2, ColorCode = 4,  ImageId=3, ImageName= "3.jpg" },
                new Product { Id = 4, ProductName = "Ceket", SizeId = 34, CategoryId = 4, ColorCode = 8, ImageId = 4, ImageName="4.jpg"},
                new Product { Id = 5, ProductName = "Mont", SizeId = 40, CategoryId = 5, ColorCode = 7 ,  ImageId = 5, ImageName="5.jpg"},
                new Product { Id = 6, ProductName = "Hırka", SizeId = 42, CategoryId = 4, ColorCode = 10,  ImageId = 6, ImageName="6.jpg" },
                new Product { Id = 7, ProductName = "Yelek", SizeId = 36, CategoryId = 2, ColorCode = 6, ImageId = 7, ImageName="7.jpg"},
                new Product { Id = 8, ProductName = "Elbise", SizeId = 38, CategoryId = 5, ColorCode = 5,  ImageId = 8, ImageName="8.jpg"},
                new Product { Id = 9, ProductName = "Tulum", SizeId = 42, CategoryId = 2, ColorCode = 3 ,  ImageId = 9, ImageName= "9.jpg" },
                new Product { Id = 10, ProductName = "Kazak", SizeId = 38, CategoryId = 1, ColorCode = 1 ,  ImageId = 10, ImageName="10.jpg" },
                new Product { Id = 11, ProductName = "Sweat", SizeId = 34, CategoryId = 4, ColorCode = 3 ,  ImageId = 11, ImageName = "11.jpg" },
                new Product { Id = 12, ProductName = "Şort", SizeId = 34, CategoryId = 6, ColorCode = 7 ,   ImageId = 12, ImageName= "12.jpg" },
                new Product { Id = 13, ProductName = "Tayt", SizeId = 36, CategoryId = 1, ColorCode = 2 ,  ImageId=13, ImageName= "13.jpg" }
                );

            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Spor" },
               new Category { Id = 2, Name = "Şık" },
               new Category { Id = 3, Name = "Dış Giyim" },
               new Category { Id = 4, Name = "Triko" },
               new Category { Id = 5, Name = "Deri" },
               new Category { Id = 6, Name = "Jean" }
               );


            modelBuilder.Entity<ProdColor>().HasData(
                        new ProdColor { Id = 1, Color = "Yeşil", ColorCode = 1 },
                        new ProdColor { Id = 2, Color = "Siyah", ColorCode = 2 },
                        new ProdColor { Id = 3, Color = "Beyaz", ColorCode = 3 },
                        new ProdColor { Id = 4, Color = "Kırmızı", ColorCode = 4 },
                        new ProdColor { Id = 5, Color = "Mor", ColorCode = 5 },
                        new ProdColor { Id = 6, Color = "Sarı", ColorCode = 6 },
                        new ProdColor { Id = 7, Color = "Gri", ColorCode = 7 },
                        new ProdColor { Id = 8, Color = "Kahverengi", ColorCode = 8 },
                        new ProdColor { Id = 9, Color = "Mavi", ColorCode = 9 },
                        new ProdColor { Id = 10, Color = "Pembe", ColorCode = 10 }

                        );

            
                


            
            




        }


    }






}
