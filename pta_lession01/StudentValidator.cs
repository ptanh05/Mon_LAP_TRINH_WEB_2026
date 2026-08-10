using System;
using System.Text.RegularExpressions;

namespace pta_lession01
{
    internal static class StudentValidator
    {
        private static readonly Regex EmailRegex = new(
            @"^[^\s@]+@[^\s@]+\.[^\s@]+$",
            RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);

        public static bool TryValidate(Student student, out string? error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(student.Id))
            {
                error = "Mã sinh viên không được để trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.FullName))
            {
                error = "Họ tên không được để trống.";
                return false;
            }

            if (student.Gpa < 0 || student.Gpa > 10)
            {
                error = "Điểm trung bình phải nằm trong khoảng từ 0 đến 10.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Email) || !EmailRegex.IsMatch(student.Email))
            {
                error = "Email không đúng định dạng.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Phone))
            {
                error = "Số điện thoại không được để trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Major))
            {
                error = "Ngành học không được để trống.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(student.Status))
            {
                error = "Trạng thái học tập không được để trống.";
                return false;
            }

            return true;
        }
    }
}
