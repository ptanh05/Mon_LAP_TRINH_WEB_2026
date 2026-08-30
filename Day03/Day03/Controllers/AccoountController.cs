using Microsoft.AspNetCore.Mvc;
using Day03.Models;
namespace Day03.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}