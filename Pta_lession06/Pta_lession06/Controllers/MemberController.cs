using Microsoft.AspNetCore.Mvc;
using Pta_lession06.Models.DataModel;
namespace Pta_lession06.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            var member = new Member
            {
                MemberId = Guid.NewGuid().ToString(),
                UserName = "phungtheanh",
                FullName = "Phung The Anh",
                Password = "password",
                Email = "phungtheanh@example.com"
            };
            ViewBag.member = member;
            return View(member);
        }
    }
}
