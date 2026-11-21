using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pta_day15_01.Models;
using System.Diagnostics;

namespace Pta_day15_01.Controllers;

public class HomeController : Controller
{
    private readonly QltaiSanContext _context;

    public HomeController(QltaiSanContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var dsTaiSan = _context.TaiSans
            .Where(t => t.NguyenGia != null && t.NguyenGia >= 20)
            .ToList();
        ViewBag.TieuDe = "Pta";
        return View(dsTaiSan);
    }

    public IActionResult FilterByLoai(int? maLoai)
    {
        List<TaiSan> dsTaiSan;
        if (maLoai == null)
        {
            dsTaiSan = _context.TaiSans
                .Where(t => t.NguyenGia != null && t.NguyenGia >= 20)
                .ToList();
            ViewBag.TieuDe = "Pta";
        }
        else
        {
            var loai = _context.LoaiTaiSans.Find(maLoai);
            dsTaiSan = _context.TaiSans
                .Where(t => t.MaLoai == maLoai)
                .ToList();
            ViewBag.TieuDe = loai?.TenLoai ?? "";
        }

        return PartialView("Pta_MainContent", dsTaiSan);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.MaLoai = new SelectList(_context.LoaiTaiSans, "MaLoai", "TenLoai");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TaiSan taiSan)
    {
        if (ModelState.IsValid)
        {
            _context.TaiSans.Add(taiSan);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.MaLoai = new SelectList(_context.LoaiTaiSans, "MaLoai", "TenLoai", taiSan.MaLoai);
        return View(taiSan);
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
