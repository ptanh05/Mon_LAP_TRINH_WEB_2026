using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pta_231220711_on_tap_de_thi_mau.Models;

namespace Pta_231220711_on_tap_de_thi_mau.Controllers
{
    public class HomeController : Controller
    {
        private readonly QlHangHoaContext _context;

        public HomeController(QlHangHoaContext context)
        {
            _context = context;
        }

        // Câu 3: Load dữ liệu từ bảng HangHoa có Gia >= 100
        public async Task<IActionResult> Index()
        {
            var hangHoas = await _context.HangHoas
                .Where(h => h.Gia >= 100)
                .ToListAsync();

            return View(hangHoas);
        }

        // Câu 4: Lọc hàng hóa theo loại bằng AJAX
        [HttpGet]
        public async Task<IActionResult> FilterByLoai(int? maLoai)
        {
            var query = _context.HangHoas.AsQueryable();

            // Điều kiện theo đề: Giá >= 100
            query = query.Where(h => h.Gia >= 100);

            if (maLoai.HasValue && maLoai.Value > 0)
            {
                query = query.Where(h => h.MaLoai == maLoai.Value);
            }

            var list = await query.ToListAsync();
            return PartialView("_HangHoaListPartial", list);
        }

        // Câu 5.1: Thêm hàng hóa mới (GET)
        public IActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai");
            return View();
        }

        // Câu 5.1: Thêm hàng hóa mới (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoai,TenHang,Gia,Anh")] HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                _context.HangHoas.Add(hangHoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MaLoai = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
