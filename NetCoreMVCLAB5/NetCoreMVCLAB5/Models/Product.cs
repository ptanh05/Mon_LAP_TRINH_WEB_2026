using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace NetCoreMVCLAB5.Models
{
    public class Product : IValidatableObject
    {
        [Display(Name = "Mã sản phẩm")]
        public int Id { get; set; }

        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm phải từ 6 đến 150 ký tự")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [NotMapped]
        [Display(Name = "Ảnh sản phẩm")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Giá chuẩn")]
        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(100000, float.MaxValue, ErrorMessage = "Giá sản phẩm phải nhỏ nhất là 100.000 VNĐ")]
        public float Price { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        [Required(ErrorMessage = "Giá khuyến mãi không được để trống")]
        public float SalePrice { get; set; }

        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Mô tả sản phẩm không được để trống")]
        [MaxLength(1500, ErrorMessage = "Mô tả không được vượt quá 1500 ký tự")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục hợp lệ")]
        public int CategoryId { get; set; }

        // Đối tượng danh mục để hiển thị thông tin
        public Category? Category { get; set; }

        // Logic kiểm tra bổ sung (IValidatableObject)
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 1. Kiểm tra SalePrice không âm
            if (SalePrice < 0)
            {
                yield return new ValidationResult(
                    "Giá khuyến mãi không được âm",
                    new[] { nameof(SalePrice) }
                );
            }

            // 2. SalePrice phải nhỏ hơn giá chuẩn ít nhất 10% (tối đa bằng 90% giá chuẩn)
            if (Price >= 100000 && SalePrice > Price * 0.9f)
            {
                yield return new ValidationResult(
                    $"Giá khuyến mãi phải nhỏ hơn giá chuẩn ít nhất 10% (tối đa là {(Price * 0.9f):N0} VNĐ)",
                    new[] { nameof(SalePrice) }
                );
            }

            // 3. Description không chứa các từ nhạy cảm (die, admin, fack...)
            if (!string.IsNullOrWhiteSpace(Description))
            {
                string[] sensitiveWords = new[] { "die", "admin", "fack", "fuck", "bitch", "dam" };
                foreach (var word in sensitiveWords)
                {
                    if (Description.Contains(word, StringComparison.OrdinalIgnoreCase))
                    {
                        yield return new ValidationResult(
                            $"Mô tả không được chứa từ nhạy cảm: \"{word}\"",
                            new[] { nameof(Description) }
                        );
                    }
                }
            }
        }
    }
}
