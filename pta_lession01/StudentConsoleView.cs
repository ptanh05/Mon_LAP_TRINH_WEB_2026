using System;
using System.Collections.Generic;
using System.Linq;

namespace pta_lession01
{
    internal static class StudentConsoleView
    {
        public static void ShowStudents(IEnumerable<Student> students)
        {
            var list = students.ToList();
            if (!list.Any())
            {
                Console.WriteLine("Không có sinh viên nào.");
                return;
            }

            Console.WriteLine("-------------------------------------------------------------------------------");
            Console.WriteLine("{0,-8} {1,-22} {2,-10} {3,-8} {4,-18} {5,-6} {6,-6}",
                "Mã", "Họ tên", "Ngày sinh", "Giới", "Email", "Điểm", "Trạng thái");
            Console.WriteLine("-------------------------------------------------------------------------------");

            foreach (var student in list)
            {
                Console.WriteLine("{0,-8} {1,-22} {2,-10:dd/MM/yyyy} {3,-8} {4,-18} {5,-6:0.00} {6,-6}",
                    student.Id,
                    Truncate(student.FullName, 22),
                    student.DateOfBirth,
                    Truncate(student.Gender, 8),
                    Truncate(student.Email, 18),
                    student.Gpa,
                    Truncate(student.Status, 6));
            }

            Console.WriteLine("-------------------------------------------------------------------------------");
        }

        public static void ShowStatisticsByMajor(IReadOnlyDictionary<string, int> counts)
        {
            if (!counts.Any())
            {
                Console.WriteLine("Không có dữ liệu ngành học.");
                return;
            }

            Console.WriteLine("Thống kê theo ngành học:");
            foreach (var item in counts)
            {
                Console.WriteLine($"- {item.Key}: {item.Value}");
            }
        }

        public static void ShowStatisticsByStatus(IReadOnlyDictionary<string, int> counts)
        {
            if (!counts.Any())
            {
                Console.WriteLine("Không có dữ liệu trạng thái.");
                return;
            }

            Console.WriteLine("Thống kê theo trạng thái:");
            foreach (var item in counts)
            {
                Console.WriteLine($"- {item.Key}: {item.Value}");
            }
        }

        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            {
                return value;
            }

            return value.Substring(0, maxLength - 3) + "...";
        }
    }
}
