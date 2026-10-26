using System.ComponentModel.DataAnnotations;

namespace Pta_Buoi_11_Lab3.Models
{
    public class Student
    {
        [Display(Name = "Mã sinh viên")]
        public int Id { get; set; } // Mã sinh viên

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ tên bắt buộc phải nhập")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải từ 4 đến 100 ký tự")]
        public string? Name { get; set; } // Họ tên

        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "Địa chỉ email phải có đuôi gmail.com")]
        public string? Email { get; set; } // Email

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu bắt buộc phải nhập")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", ErrorMessage = "Mật khẩu từ 8 ký tự trở lên, có ký tự viết hoa, viết thường, chữ số và ký tự đặc biệt")]
        public string? Password { get; set; } // Mật khẩu

        [Display(Name = "Ngành học")]
        [Required(ErrorMessage = "Ngành học bắt buộc phải chọn")]
        public Branch? Branch { get; set; } // Ngành học

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Giới tính bắt buộc phải chọn")]
        public Gender? Gender { get; set; } // Giới tính

        [Display(Name = "Hệ chính quy")]
        public bool IsRegular { get; set; } // Hệ: true-chính quy, false-phi chính quy

        [Display(Name = "Địa chỉ")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Địa chỉ bắt buộc phải nhập")]
        public string? Address { get; set; } // Địa chỉ

        [Display(Name = "Ngày sinh")]
        [Range(typeof(DateTime), "1/1/1963", "12/31/2005", ErrorMessage = "Ngày sinh phải nằm trong khoảng từ 01/01/1963 đến 31/12/2005")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Ngày sinh bắt buộc phải nhập")]
        public DateTime? DateOfBorth { get; set; } // Ngày sinh

        [Display(Name = "Điểm")]
        [Required(ErrorMessage = "Điểm bắt buộc phải nhập")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm phải là số thực từ 0.0 đến 10.0")]
        public double? Score { get; set; } // Điểm
    }
}
