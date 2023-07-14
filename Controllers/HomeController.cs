using BoutiqueProje.Data;
using BoutiqueProje.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Diagnostics;

namespace BoutiqueProje.Controllers
{
    public class HomeController : Controller
    {       
        private readonly ILogger<HomeController> _logger;
        private readonly BoutiqueProductContext _db;

        public HomeController(ILogger<HomeController> logger, BoutiqueProductContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()

        {

            var boutiqueProductContext = _db.Products.Include(p => p.Category);

            var lst_product = from p in _db.Products
                             .Include(p => p.Category)
                             .Include(p => p.ProdColors)                             
                              select new ProductViewModel()
                              {
                                  Id = p.Id,
                                  ProductName = p.ProductName,
                                  Color = p.Color,
                                  Size = p.SizeId,
                                  CategoryId = p.Category.Id,
                                  ImageName = p.ImageName
                              };

            //var boutiqueProductContext = new ProductViewModel();
            //var lst_product = _db.Products.Where(p => p.Name == p.ProductName)
            //    .Where(p=> p.Category == p.Category)
            //    .FirstOrDefault();                             
            //List<Product> products = (from p in _db.Products.ToList()
            //                          select new Product
            //                          {
            //                              ProductName = p.Name,
            //                              Category = p.Category,
            //                              Image = p.Image,
            //                              Color = p.Color

            //                          }).ToList();                           




            //var product = _db.Products.ToList();                
            //return View(product);
            //List<Product> products = new List<Product>();  // Assuming this returns a List<Product>
            //List<ProductViewModel> productViewModels = new List<ProductViewModel>();

            //foreach (var p in products)
            //{
            //    var productViewModel = new ProductViewModel
            //    {
            //        // Map properties from Product to ProductViewModel as needed
            //        ProductName = p.Name,
            //        CategoryId = p.Category.Id,
            //        Image = p.Image,
            //        Color = p.Color
            //        // ...
            //    };

            //    productViewModels.Add(productViewModel);
            //}
            //var lst_product = _db.Products.Select(p => p.Name == p.ProductName).Select(p => p.Image)
            //.Select(p => p.Color).ToList();
            return View(_db.Products.ToList());

        }
        

        public IActionResult Details(int id)
        {
            return View(_db.Products.Where(i=> i.Id == id).FirstOrDefault());
        }
        public IActionResult List()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}