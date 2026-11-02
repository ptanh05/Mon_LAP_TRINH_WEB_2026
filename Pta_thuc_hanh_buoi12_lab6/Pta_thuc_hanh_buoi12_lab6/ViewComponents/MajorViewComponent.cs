using Microsoft.AspNetCore.Mvc;
using Pta_thuc_hanh_buoi12_lab6.Data;
using Pta_thuc_hanh_buoi12_lab6.Models;

namespace Pta_thuc_hanh_buoi12_lab6.ViewComponents
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