using Microsoft.AspNetCore.Mvc;
using Pta_thuc_hanh_buoi5_lab5.Models;
using System.Diagnostics;

namespace Pta_thuc_hanh_buoi5_lab5.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
