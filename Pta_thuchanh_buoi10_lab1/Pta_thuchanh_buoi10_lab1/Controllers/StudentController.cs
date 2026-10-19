using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pta_thuchanh_buoi10_lab1.Models;

namespace Pta_thuchanh_buoi10_lab1.Controllers
{
    public class StudentController : Controller
    {
        // Dùng static để danh sách duy trì qua các request
        private static List<Student> listStudents = new List<Student>()
        {
            new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                Gender = Gender.Male, IsRegular = true,
                Address = "A1-2018", Email = "nam@g.com", DateOfBorth = new DateTime(2003, 1, 1) },
            new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE,
                Gender = Gender.Female, IsRegular = true,
                Address = "A1-2019", Email = "tu@g.com", DateOfBorth = new DateTime(2003, 2, 2) },
            new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE,
                Gender = Gender.Male, IsRegular = false,
                Address = "A1-2020", Email = "phong@g.com", DateOfBorth = new DateTime(2003, 3, 3) },
            new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
                Gender = Gender.Female, IsRegular = false,
                Address = "A1-2021", Email = "mai@g.com", DateOfBorth = new DateTime(2003, 4, 4) }
        };

        private readonly IWebHostEnvironment _env;

        public StudentController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // Mục 8: Cấu hình Route thành Admin/Student/List (hỗ trợ cả đường dẫn mặc định Student/Index)
        [Route("Admin/Student/List")]
        [Route("Student/Index")]
        [Route("Student")]
        public IActionResult Index()
        {
            return View(listStudents);
        }

        // Mục 8: Cấu hình Route thành Admin/Student/Add
        [HttpGet]
        [Route("Admin/Student/Add")]
        [Route("Student/Create")]
        public IActionResult Create()
        {
            // Danh sách giới tính cho radio button
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            // Danh sách các ngành học cho dropdown list
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "IT" },
                new SelectListItem { Text = "BE", Value = "BE" },
                new SelectListItem { Text = "CE", Value = "CE" },
                new SelectListItem { Text = "EE", Value = "EE" }
            };

            return View();
        }

        [HttpPost]
        [Route("Admin/Student/Add")]
        [Route("Student/Create")]
        public async Task<IActionResult> Create(Student s)
        {
            // Mục 9: Xử lý upload ảnh đại diện
            if (s.AvatarFile != null && s.AvatarFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images", "avatars");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(s.AvatarFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await s.AvatarFile.CopyToAsync(fileStream);
                }

                s.Avatar = "/images/avatars/" + uniqueFileName;
            }

            // Tự động tăng Id
            s.Id = listStudents.Any() ? listStudents.Last().Id + 1 : 101;
            listStudents.Add(s);

            return View("Index", listStudents);
        }
    }
}
