using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pta_231220711_on_tap_de_thi_mau.Models;

namespace Pta_231220711_on_tap_de_thi_mau.ViewComponents
{
    public class LoaiHangMenuViewComponent : ViewComponent
    {
        private readonly QlHangHoaContext _context;

        public LoaiHangMenuViewComponent(QlHangHoaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loaiHangs = await _context.LoaiHangs.ToListAsync();
            return View(loaiHangs);
        }
    }
}

