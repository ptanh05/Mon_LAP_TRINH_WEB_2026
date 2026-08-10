using System;
using System.Collections.Generic;
using System.Linq;

namespace pta_lession01
{
    internal class StudentService
    {
        private readonly List<Student> _students = new();

        public IReadOnlyList<Student> Students => _students;

        public bool Add(Student student, out string? error)
        {
            error = null;
            if (GetById(student.Id) is not null)
            {
                error = $"Mã sinh viên '{student.Id}' đã tồn tại.";
                return false;
            }

            if (!StudentValidator.TryValidate(student, out error))
            {
                return false;
            }

            _students.Add(student);
            return true;
        }

        public Student? GetById(string id)
        {
            return _students.FirstOrDefault(s => s.Id.Equals(id.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Student> SearchByName(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Array.Empty<Student>();
            }

            string normalized = text.Trim();
            return _students
                .Where(s => s.FullName.Contains(normalized, StringComparison.OrdinalIgnoreCase))
                .OrderBy(s => s.FullName)
                .ToList();
        }

        public bool Update(string id, Student updatedStudent, out string? error)
        {
            error = null;
            var existing = GetById(id);
            if (existing is null)
            {
                error = $"Không tìm thấy sinh viên có mã '{id}'.";
                return false;
            }

            if (!updatedStudent.Id.Equals(id, StringComparison.OrdinalIgnoreCase)
                && GetById(updatedStudent.Id) is not null)
            {
                error = $"Mã sinh viên '{updatedStudent.Id}' đã tồn tại.";
                return false;
            }

            if (!StudentValidator.TryValidate(updatedStudent, out error))
            {
                return false;
            }

            existing.FullName = updatedStudent.FullName;
            existing.DateOfBirth = updatedStudent.DateOfBirth;
            existing.Gender = updatedStudent.Gender;
            existing.Email = updatedStudent.Email;
            existing.Phone = updatedStudent.Phone;
            existing.Major = updatedStudent.Major;
            existing.Gpa = updatedStudent.Gpa;
            existing.Status = updatedStudent.Status;
            existing.SetId(updatedStudent.Id);
            return true;
        }

        public bool Delete(string id, out string? error)
        {
            error = null;
            var student = GetById(id);
            if (student is null)
            {
                error = $"Không tìm thấy sinh viên có mã '{id}'.";
                return false;
            }

            _students.Remove(student);
            return true;
        }

        public IReadOnlyList<Student> SortByName(bool ascending = true)
        {
            return ascending
                ? _students.OrderBy(s => s.FullName).ToList()
                : _students.OrderByDescending(s => s.FullName).ToList();
        }

        public IReadOnlyList<Student> SortByGpa(bool ascending = true)
        {
            return ascending
                ? _students.OrderBy(s => s.Gpa).ToList()
                : _students.OrderByDescending(s => s.Gpa).ToList();
        }

        public IEnumerable<Student> GetHighAchievers()
        {
            return _students.Where(s => s.Gpa >= 8.0).OrderByDescending(s => s.Gpa).ThenBy(s => s.FullName);
        }

        public IEnumerable<Student> GetTopStudents()
        {
            if (!_students.Any())
            {
                return Array.Empty<Student>();
            }

            double maxGpa = _students.Max(s => s.Gpa);
            return _students.Where(s => Math.Abs(s.Gpa - maxGpa) < 0.0001).ToList();
        }

        public double GetAverageGpa()
        {
            return _students.Any() ? _students.Average(s => s.Gpa) : 0.0;
        }

        public IReadOnlyDictionary<string, int> CountByMajor()
        {
            return _students
                .GroupBy(s => s.Major)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public IReadOnlyDictionary<string, int> CountByStatus()
        {
            return _students
                .GroupBy(s => s.Status)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public void SeedSampleData()
        {
            Add(new Student("SV001", "Nguyễn Văn A", new DateOnly(2003, 2, 20), "Nam", "nguyenvana@example.com", "0912345678", "Công nghệ thông tin", 8.6, "Đang học"), out _);
            Add(new Student("SV002", "Trần Thị B", new DateOnly(2004, 3, 14), "Nữ", "tranthib@example.com", "0987654321", "Kinh tế", 7.4, "Đang học"), out _);
            Add(new Student("SV003", "Lê Văn C", new DateOnly(2002, 10, 5), "Nam", "levanc@example.com", "0909123456", "Quản trị kinh doanh", 9.2, "Hoàn thành"), out _);
        }
    }
}
