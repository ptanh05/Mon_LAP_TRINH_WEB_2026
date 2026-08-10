using System;

namespace pta_lession01
{
    internal static class Program
    {
        private static void Main()
        {
            var service = new StudentService();
            service.SeedSampleData();

            while (true)
            {
                MenuManager.ShowMenu();
                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice?.Trim())
                {
                    case "1":
                        AddStudent(service);
                        break;
                    case "2":
                        StudentConsoleView.ShowStudents(service.Students);
                        break;
                    case "3":
                        FindById(service);
                        break;
                    case "4":
                        SearchByName(service);
                        break;
                    case "5":
                        UpdateStudent(service);
                        break;
                    case "6":
                        DeleteStudent(service);
                        break;
                    case "7":
                        SortByName(service);
                        break;
                    case "8":
                        SortByGpa(service);
                        break;
                    case "9":
                        ShowHighAchievers(service);
                        break;
                    case "10":
                        ShowTopStudents(service);
                        break;
                    case "11":
                        ShowAverageGpa(service);
                        break;
                    case "12":
                        StudentConsoleView.ShowStatisticsByMajor(service.CountByMajor());
                        break;
                    case "13":
                        StudentConsoleView.ShowStatisticsByStatus(service.CountByStatus());
                        break;
                    case "0":
                        return;
                    default:
                        MenuManager.ShowError("Lựa chọn không hợp lệ.");
                        break;
                }
            }
        }

        private static void AddStudent(StudentService service)
        {
            Console.WriteLine("Thêm sinh viên mới");
            var student = ReadStudentInput();
            if (student is null)
            {
                return;
            }

            if (service.Add(student, out string? error))
            {
                MenuManager.ShowSuccess("Đã thêm sinh viên thành công.");
            }
            else
            {
                MenuManager.ShowError(error ?? "Không thể thêm sinh viên.");
            }
        }

        private static void FindById(StudentService service)
        {
            Console.Write("Nhập mã sinh viên cần tìm: ");
            string? id = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(id))
            {
                MenuManager.ShowError("Mã sinh viên không được để trống.");
                return;
            }

            var student = service.GetById(id);
            if (student is null)
            {
                MenuManager.ShowError("Không tìm thấy sinh viên.");
                return;
            }

            StudentConsoleView.ShowStudents(new[] { student });
        }

        private static void SearchByName(StudentService service)
        {
            Console.Write("Nhập họ tên hoặc một phần họ tên: ");
            string? text = Console.ReadLine();
            var result = service.SearchByName(text ?? string.Empty);
            StudentConsoleView.ShowStudents(result);
        }

        private static void UpdateStudent(StudentService service)
        {
            Console.Write("Nhập mã sinh viên cần cập nhật: ");
            string? id = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(id))
            {
                MenuManager.ShowError("Mã sinh viên không được để trống.");
                return;
            }

            var existing = service.GetById(id);
            if (existing is null)
            {
                MenuManager.ShowError("Không tìm thấy sinh viên.");
                return;
            }

            Console.WriteLine("Nhập thông tin mới (để trống nếu không đổi):");
            var updatedStudent = ReadStudentInput(existing);
            if (updatedStudent is null)
            {
                return;
            }

            if (service.Update(id, updatedStudent, out string? error))
            {
                MenuManager.ShowSuccess("Cập nhật sinh viên thành công.");
            }
            else
            {
                MenuManager.ShowError(error ?? "Không thể cập nhật sinh viên.");
            }
        }

        private static void DeleteStudent(StudentService service)
        {
            Console.Write("Nhập mã sinh viên cần xóa: ");
            string? id = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(id))
            {
                MenuManager.ShowError("Mã sinh viên không được để trống.");
                return;
            }

            if (service.Delete(id, out string? error))
            {
                MenuManager.ShowSuccess("Đã xóa sinh viên thành công.");
            }
            else
            {
                MenuManager.ShowError(error ?? "Không thể xóa sinh viên.");
            }
        }

        private static void SortByName(StudentService service)
        {
            Console.Write("Chọn thứ tự (1. Tăng dần, 2. Giảm dần): ");
            string? option = Console.ReadLine();
            bool ascending = option?.Trim() != "2";
            StudentConsoleView.ShowStudents(service.SortByName(ascending));
        }

        private static void SortByGpa(StudentService service)
        {
            Console.Write("Chọn thứ tự (1. Tăng dần, 2. Giảm dần): ");
            string? option = Console.ReadLine();
            bool ascending = option?.Trim() != "2";
            StudentConsoleView.ShowStudents(service.SortByGpa(ascending));
        }

        private static void ShowHighAchievers(StudentService service)
        {
            StudentConsoleView.ShowStudents(service.GetHighAchievers());
        }

        private static void ShowTopStudents(StudentService service)
        {
            StudentConsoleView.ShowStudents(service.GetTopStudents());
        }

        private static void ShowAverageGpa(StudentService service)
        {
            double average = service.GetAverageGpa();
            Console.WriteLine($"Điểm trung bình toàn bộ sinh viên: {average:0.00}");
        }

        private static Student? ReadStudentInput(Student? existing = null)
        {
            string id = ReadField("Mã sinh viên", existing?.Id);
            string fullName = ReadField("Họ tên", existing?.FullName);
            DateOnly dob = ReadDate("Ngày sinh", existing?.DateOfBirth);
            string gender = ReadField("Giới tính", existing?.Gender);
            string email = ReadField("Email", existing?.Email);
            string phone = ReadField("Số điện thoại", existing?.Phone);
            string major = ReadField("Ngành học", existing?.Major);
            double gpa = ReadDouble("Điểm trung bình", existing?.Gpa);
            string status = ReadField("Trạng thái học tập", existing?.Status);

            try
            {
                return new Student(id, fullName, dob, gender, email, phone, major, gpa, status);
            }
            catch (Exception ex)
            {
                MenuManager.ShowError($"Lỗi nhập dữ liệu: {ex.Message}");
                return null;
            }
        }

        private static string ReadField(string label, string? existingValue)
        {
            Console.Write($"{label}{(existingValue is null ? string.Empty : $" [{existingValue}]")}: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) && existingValue is not null)
            {
                return existingValue;
            }

            return input?.Trim() ?? string.Empty;
        }

        private static DateOnly ReadDate(string label, DateOnly? existingValue)
        {
            while (true)
            {
                Console.Write($"{label}{(existingValue.HasValue ? $" [{existingValue:dd/MM/yyyy}]" : string.Empty)}: ");
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) && existingValue.HasValue)
                {
                    return existingValue.Value;
                }

                if (DateOnly.TryParse(input, out DateOnly date))
                {
                    return date;
                }

                MenuManager.ShowError("Định dạng ngày không hợp lệ. Vui lòng nhập lại (dd/MM/yyyy)." );
            }
        }

        private static double ReadDouble(string label, double? existingValue)
        {
            while (true)
            {
                Console.Write($"{label}{(existingValue.HasValue ? $" [{existingValue:0.00}]" : string.Empty)}: ");
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) && existingValue.HasValue)
                {
                    return existingValue.Value;
                }

                if (double.TryParse(input, out double value))
                {
                    return value;
                }

                MenuManager.ShowError("Giá trị không hợp lệ. Vui lòng nhập số.");
            }
        }
    }
}
