using System;

namespace pta_lession01
{
    internal static class MenuManager
    {
        public static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("QUẢN LÝ SINH VIÊN");
            Console.WriteLine("1. Thêm sinh viên");
            Console.WriteLine("2. Hiển thị danh sách");
            Console.WriteLine("3. Tìm sinh viên theo mã");
            Console.WriteLine("4. Tìm gần đúng theo họ tên");
            Console.WriteLine("5. Cập nhật sinh viên");
            Console.WriteLine("6. Xóa sinh viên");
            Console.WriteLine("7. Sắp xếp theo họ tên");
            Console.WriteLine("8. Sắp xếp theo điểm trung bình");
            Console.WriteLine("9. Hiển thị sinh viên có điểm từ 8 trở lên");
            Console.WriteLine("10. Hiển thị sinh viên có điểm cao nhất");
            Console.WriteLine("11. Tính điểm trung bình toàn bộ sinh viên");
            Console.WriteLine("12. Thống kê sinh viên theo ngành");
            Console.WriteLine("13. Thống kê sinh viên theo trạng thái");
            Console.WriteLine("0. Thoát");
            Console.Write("Chọn chức năng: ");
        }

        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
