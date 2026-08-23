using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using System.Diagnostics;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product Name 1", Image = "/images/bag1.jpg" },
                new Product { Id = 2, Name = "Product Name 2", Image = "/images/bag2.jpg" },
                new Product { Id = 3, Name = "Product Name 3", Image = "/images/bag3.jpg" },
                new Product { Id = 4, Name = "Product Name 4", Image = "/images/bag4.jpg" }
            };
            return View(products);
        }

        public IActionResult Privacy()
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
