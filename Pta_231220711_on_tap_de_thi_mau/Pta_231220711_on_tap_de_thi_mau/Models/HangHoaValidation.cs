using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Pta_231220711_on_tap_de_thi_mau.Models
{
    [ModelMetadataType(typeof(HangHoaMetadata))]
    public partial class HangHoa
    {
    }

    public class HangHoaMetadata
    {
        [Display(Name = "Loại hàng")]
        [Required(ErrorMessage = "Vui lòng chọn loại hàng")]
        public int MaLoai { get; set; }

        [Display(Name = "Tên hàng")]
        [Required(ErrorMessage = "Tên hàng không được để trống")]
        [StringLength(50, ErrorMessage = "Tên hàng tối đa 50 ký tự")]
        public string TenHang { get; set; } = null!;

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(100, 5000, ErrorMessage = "Giá phải là số và nằm trong giới hạn 100 đến 5000")]
        public decimal? Gia { get; set; }

        [Display(Name = "Ảnh")]
        [Required(ErrorMessage = "Tên file ảnh không được để trống")]
        [RegularExpression(@"^.+\.(jpg|JPG|png|PNG|gif|GIF|tiff|TIFF)$", 
            ErrorMessage = "Tên file ảnh phải có đuôi: .jpg | .png | .gif | .tiff")]
        public string? Anh { get; set; }

        [ValidateNever]
        public virtual LoaiHang MaLoaiNavigation { get; set; } = null!;
    }
}

