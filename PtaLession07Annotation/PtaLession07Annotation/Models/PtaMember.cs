using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PtaLession07Annotation.Models
{
    public class PtaMember
    {
        
        public int Id { get; set; }
        [DisplayName("Tai khoan")]
        [Required(ErrorMessage = "Tai khoan khong duoc de trong")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Tai khoan phai tu 3 den 20 ky tu")]
        public string PtaUsername { get; set; }
        [DisplayName("Mat khau")]
        [StringLength(100, MinimumLength =8 , ErrorMessage = "Mat khau toi thieu 8 ky tu")]
        public string PtaPassword { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage = "Email khong duoc de trong")]
        [DataType(DataType.EmailAddress)]
        public string PtaEmail { get; set; }
        [DisplayName("So dien thoai")]
        [Required(ErrorMessage = "Ban chua nhap sdt")]
        [RegularExpression(@"^0\d{9,9}",ErrorMessage = "dien thoai la 10 ky tu so, bat dau la so 0 ")]
        public string PtaPhone { get; set; }
    }
}
