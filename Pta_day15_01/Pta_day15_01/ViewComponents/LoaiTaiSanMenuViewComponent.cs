using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pta_day15_01.Models;

namespace Pta_day15_01.ViewComponents;

public class LoaiTaiSanMenuViewComponent : ViewComponent
{
    private readonly QltaiSanContext _context;

    public LoaiTaiSanMenuViewComponent(QltaiSanContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var loaiTaiSans = await _context.LoaiTaiSans.ToListAsync();
        return View(loaiTaiSans);
    }
}
