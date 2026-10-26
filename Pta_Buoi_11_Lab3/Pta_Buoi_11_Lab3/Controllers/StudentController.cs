using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pta_Buoi_11_Lab3.Models;

namespace Pta_Buoi_11_Lab3.Controllers
{
    public class StudentController : Controller
    {
        private static List<Student> listStudents = new List<Student>()
        {
            new Student() {
                Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                Gender = Gender.Male, IsRegular = true,
                Address = "A1-2018", Email = "nam@gmail.com",
                DateOfBorth = new DateTime(2003, 1, 1),
                Score = 8.5
            },
            new Student() {
                Id = 102, Name = "Minh Tú", Branch = Branch.BE,
                Gender = Gender.Female, IsRegular = true,
                Address = "A1-2019", Email = "tu@gmail.com",
                DateOfBorth = new DateTime(2003, 2, 2),
                Score = 7.0
            },
            new Student() {
                Id = 103, Name = "Hoàng Phong", Branch = Branch.CE,
                Gender = Gender.Male, IsRegular = false,
                Address = "A1-2020", Email = "phong@gmail.com",
                DateOfBorth = new DateTime(2003, 3, 3),
                Score = 9.0
            },
            new Student() {
                Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
                Gender = Gender.Female, IsRegular = false,
                Address = "A1-2021", Email = "mai@gmail.com",
                DateOfBorth = new DateTime(2003, 4, 4),
                Score = 6.5
            }
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View(listStudents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };

            return View();
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                s.Id = listStudents.Any() ? listStudents.Last<Student>().Id + 1 : 101;
                listStudents.Add(s);
                return View("Index", listStudents);
            }

            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };

            return View(s);
        }
    }
}
