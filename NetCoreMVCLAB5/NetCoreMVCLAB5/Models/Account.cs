using System;

using System.ComponentModel.DataAnnotations;

namespace NetCoreMVCLAB5.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [
            Display(Name = "Full Name"),
            Required(ErrorMessage = "Full Name is required"),
            MinLength(3, ErrorMessage = "Full Name must be at least 3 characters long"),
            MaxLength(50, ErrorMessage = "Full Name cannot exceed 50 characters")
        ]
        public string FullName { get; set; } = string.Empty;

        [
            Display(Name = "Email"),
            Required(ErrorMessage = "Email is required"),
            EmailAddress(ErrorMessage = "Invalid email format")
        ]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [Remove(action: "VerifyPhone", Controller: "Account", ErrorMessage = "Phone number is required")]
        public string? Phone { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "Avatar")]
        public string? Avatar { get; set; }

        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [
            Display(Name = "Password"),
            Required(ErrorMessage = "Password is required"),
            MinLength(6, ErrorMessage = "Password must be at least 6 characters long")
        ]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Facebook")]
        public string? Facebook { get; set; }
    }
}
