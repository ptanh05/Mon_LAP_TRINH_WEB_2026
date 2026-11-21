using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Pta_day15_01.Models;

public partial class TaiSan
{
    public int MaTs { get; set; }

    public int MaLoai { get; set; }

    [Required(ErrorMessage = "Tên tài sản không được để trống")]
    [StringLength(100, ErrorMessage = "Tên tài sản tối đa 100 ký tự")]
    public string TenTs { get; set; } = null!;

    [Required(ErrorMessage = "Nguyên giá phải là số từ 1 đến 2000")]
    [Range(1.0, 2000.0, ErrorMessage = "Nguyên giá phải là số từ 1 đến 2000")]
    public decimal? NguyenGia { get; set; }

    [Required(ErrorMessage = "Tên file ảnh không được để trống")]
    [StringLength(100, ErrorMessage = "Tên file ảnh tối đa 100 ký tự")]
    [RegularExpression(@"^.+\.([jJ][pP][gG]|[jJ][pP][eE][gG]|[pP][nN][gG]|[wW][eE][bB][pP])$",
        ErrorMessage = "Ảnh phải có đuôi .jpg, .jpeg, .png hoặc .webp")]
    public string? Anh { get; set; }

    [ValidateNever]
    public virtual LoaiTaiSan MaLoaiNavigation { get; set; } = null!;
}
