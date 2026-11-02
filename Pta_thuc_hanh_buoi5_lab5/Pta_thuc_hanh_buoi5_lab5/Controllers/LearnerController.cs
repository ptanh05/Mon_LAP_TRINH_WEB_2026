using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pta_thuc_hanh_buoi5_lab5.Data;
using Pta_thuc_hanh_buoi5_lab5.Models;

namespace Pta_thuc_hanh_buoi5_lab5.Controllers
{
    public class LearnerController : Controller
    {
        private readonly SchoolContext db;

        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        // khai báo biến toàn cục pageSize
        private int pageSize = 3;

        public IActionResult Index(int? mid, string? keyword)
        {
            var learners = (IQueryable<Learner>)db.Learners
                .Include(m => m.Major);
            if (mid != null)
            {
                learners = learners.Where(l => l.MajorID == mid);
                ViewBag.mid = mid;
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                learners = learners.Where(l => l.FirstMidName.ToLower().Contains(keyword.ToLower())
                                            || l.LastName.ToLower().Contains(keyword.ToLower()));
                ViewBag.keyword = keyword;
            }
            // tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            // trả số trang về view để hiển thị nav-trang
            ViewBag.pageNum = pageNum;
            // lấy dữ liệu trang đầu
            var result = learners.Take(pageSize).ToList();
            return View(result);
        }

        [HttpGet]
        [Route("LearnerFilter")]
        [Route("Learner/LearnerFilter")]
        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            // lấy toàn bộ learners trong dbset chuyển về IQueryable<Learner> để query
            var learners = (IQueryable<Learner>)db.Learners;
            // lấy chỉ số trang, nếu chỉ số trang null thì gán ngầm định bằng 1
            int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);
            // nếu có mid thì lọc learner theo mid (chuyên ngành)
            if (mid != null)
            {
                // lọc
                learners = learners.Where(l => l.MajorID == mid);
                // gửi mid về view để ghi lại trên nav-phân trang
                ViewBag.mid = mid;
            }
            // nếu có keyword thì tìm kiếm theo tên hoặc họ
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                // tìm kiếm
                learners = learners.Where(l => l.FirstMidName.ToLower().Contains(keyword.ToLower())
                                            || l.LastName.ToLower().Contains(keyword.ToLower()));
                // gửi keyword về view để ghi trên nav-phân trang
                ViewBag.keyword = keyword;
            }
            // tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            // gửi số trang về view để hiển thị nav-trang
            ViewBag.pageNum = pageNum;
            // chọn dữ liệu trong trang hiện tại
            var result = learners.Skip(pageSize * (page - 1))
                .Take(pageSize).Include(m => m.Major);
            return PartialView("LearnerTable", result);
        }

        // 2.2.1 Action LearnerByMajorID trả về PartialView phục vụ AJAX
        [HttpGet]
        [Route("LearnerByMajorID")]
        [Route("Learner/LearnerByMajorID")]
        public IActionResult LearnerByMajorID(int mid)
        {
            var learners = db.Learners
                .Where(l => l.MajorID == mid)
                .Include(m => m.Major).ToList();
            return PartialView("LearnerTable", learners);
        }

        // 8.2 Create (C) - Thêm một Learner
        public IActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        // 8.3 Update (U) - Sửa một Learner
        public IActionResult Edit(int? id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        private bool LearnerExists(int id)
        {
            return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
        }

        // 8.4 Delete (D) - Xóa một Learner
        public IActionResult Delete(int? id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Include(l => l.Major)
                .Include(e => e.Enrollments)
                .FirstOrDefault(m => m.LearnerID == id);

            if (learner == null)
            {
                return NotFound();
            }
            if (learner.Enrollments.Count() > 0)
            {
                return Content("This learner has some enrollments, can't delete!");
            }
            return View(learner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Learners == null)
            {
                return Problem("Entity set 'Learners' is null.");
            }
            var learner = db.Learners.Find(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
