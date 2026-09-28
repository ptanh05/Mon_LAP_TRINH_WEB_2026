using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PtaLession07Annotation.Models;

namespace PtaLession07Annotation.Controllers
{
    public class PtaMemberController : Controller
    {
        private static List<PtaMember> ptaMembers = new List<PtaMember>()
        {
            new PtaMember { Id = 1, PtaUsername = "admin", PtaPassword = "password123", PtaEmail = "admin@gmail.com", PtaPhone = "0987654321" },
            new PtaMember { Id = 2, PtaUsername = "ptanh", PtaPassword = "password123", PtaEmail = "ptanh@gmail.com", PtaPhone = "0912345678" }
        };

        // GET: PtaMember
        public ActionResult Index()
        {
            return View(ptaMembers);
        }

        // GET: PtaMember/Details/5
        public ActionResult Details(int id)
        {
            var member = ptaMembers.FirstOrDefault(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: PtaMember/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PtaMember/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PtaMember ptaMember)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(ptaMember);
                }

                // Tự động tăng Id
                ptaMember.Id = ptaMembers.Count > 0 ? ptaMembers.Max(m => m.Id) + 1 : 1;
                ptaMembers.Add(ptaMember);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(ptaMember);
            }
        }

        // GET: PtaMember/Edit/5
        public ActionResult Edit(int id)
        {
            var member = ptaMembers.FirstOrDefault(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: PtaMember/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PtaMember ptaMember)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(ptaMember);
                }

                var existing = ptaMembers.FirstOrDefault(m => m.Id == id);
                if (existing != null)
                {
                    existing.PtaUsername = ptaMember.PtaUsername;
                    existing.PtaPassword = ptaMember.PtaPassword;
                    existing.PtaEmail = ptaMember.PtaEmail;
                    existing.PtaPhone = ptaMember.PtaPhone;
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(ptaMember);
            }
        }

        // GET: PtaMember/Delete/5
        public ActionResult Delete(int id)
        {
            var member = ptaMembers.FirstOrDefault(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: PtaMember/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var member = ptaMembers.FirstOrDefault(m => m.Id == id);
                if (member != null)
                {
                    ptaMembers.Remove(member);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
