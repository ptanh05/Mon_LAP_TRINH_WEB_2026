using Microsoft.AspNetCore.Mvc;
using Pta_lession06.Models;

namespace Pta_lession06.Controllers
{
    public class PtamemberController : Controller
    {
        // Danh sách thành viên lưu trong bộ nhớ (Mock Data mô phỏng Database)
        private static List<Ptamember> listMembers = new List<Ptamember>()
        {
            new Ptamember()
            {
                PtamemberId = "PTA001",
                PtamemberUserName = "phungtheanh",
                PtamemberPassword = "123456",
                PtamemberFullName = "Phùng Thế Anh",
                PtamemberEmail = "anh018031@gmail.com"
            },
            new Ptamember()
            {
                PtamemberId = "PTA002",
                PtamemberUserName = "nguyenvana",
                PtamemberPassword = "password123",
                PtamemberFullName = "Nguyễn Văn A",
                PtamemberEmail = "vana@gmail.com"
            },
            new Ptamember()
            {
                PtamemberId = "PTA003",
                PtamemberUserName = "tranthib",
                PtamemberPassword = "password456",
                PtamemberFullName = "Trần Thị B",
                PtamemberEmail = "thib@gmail.com"
            }
        };

        // 1. Danh sách (List): Hiển thị danh sách các thành viên
        public IActionResult Index()
        {
            return View(listMembers);
        }

        // 2. Chi tiết (Details / PtaGetDetails): Minh họa truyền đối tượng & Strongly Typed View
        public IActionResult PtaGetDetails(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var defaultMember = listMembers.FirstOrDefault() ?? new Ptamember()
                {
                    PtamemberId = "PTA001",
                    PtamemberUserName = "phungtheanh",
                    PtamemberPassword = "123456",
                    PtamemberFullName = "Phùng Thế Anh",
                    PtamemberEmail = "anh018031@gmail.com"
                };
                return View(defaultMember);
            }

            var member = listMembers.FirstOrDefault(m => m.PtamemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // 3. Tạo mới (Create - GET): Hiển thị form nhập liệu
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Tạo mới (Create - POST): Nhận dữ liệu qua Model Binding và lưu trữ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ptamember member)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(member.PtamemberId))
                {
                    member.PtamemberId = "PTA" + (listMembers.Count + 1).ToString("D3");
                }
                listMembers.Add(member);
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // 4. Chỉnh sửa (Edit - GET): Lấy thông tin thành viên theo ID
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var member = listMembers.FirstOrDefault(m => m.PtamemberId == id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // Chỉnh sửa (Edit - POST): Cập nhật dữ liệu cho bản ghi cụ thể thông qua Model Binding
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ptamember member)
        {
            if (ModelState.IsValid)
            {
                var existingMember = listMembers.FirstOrDefault(m => m.PtamemberId == member.PtamemberId);
                if (existingMember != null)
                {
                    existingMember.PtamemberUserName = member.PtamemberUserName;
                    existingMember.PtamemberFullName = member.PtamemberFullName;
                    existingMember.PtamemberPassword = member.PtamemberPassword;
                    existingMember.PtamemberEmail = member.PtamemberEmail;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // 5. Xóa (Delete): Xóa thành viên theo ID
        public IActionResult Delete(string id)
        {
            var member = listMembers.FirstOrDefault(m => m.PtamemberId == id);
            if (member != null)
            {
                listMembers.Remove(member);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
