using Microsoft.AspNetCore.Mvc;
using Pta_thuc_hanh_buoi5_lab5.Data;
using Pta_thuc_hanh_buoi5_lab5.Models;

namespace Pta_thuc_hanh_buoi5_lab5.ViewComponents
{
    public class MajorViewComponent : ViewComponent
    {
        SchoolContext db;
        List<Major> majors;

        public MajorViewComponent(SchoolContext _context)
        {
            db = _context;
            majors = db.Majors.ToList();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderMajor", majors);
        }
    }
}