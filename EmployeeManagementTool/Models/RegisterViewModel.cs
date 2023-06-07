using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementTool.Models
{
    public class RegisterViewModel
    {
        
        [Required]
        public string Firstname { get; set; } = null!;

        public string? Lastname { get; set; }
        [Required]
        public string Email { get; set; } = null!;

        public DateTime? Dob { get; set; }

        public string? Gender { get; set; }
        [Required(ErrorMessage = "Please Fill Password")]
        [MinLength(8, ErrorMessage = "Password Minimum 8 Char")]
        [RegularExpression("^(?=.*?[A-Za-z]{3,})(?=.*?[0-9]{2,})(?=.*?[#?!@$%^&*-]{2,}).{8,}$", ErrorMessage = "Password Contains 3 Char, 2 number and 2 Special Char.")]
        public string Password { get; set; } = null!;
        [NotMapped]
        [Required(ErrorMessage = "Please Fill Confirm Password")]
        [MinLength(8, ErrorMessage = "Password Minimum 8 Char")]
        [RegularExpression("^(?=.*?[A-Za-z]{3,})(?=.*?[0-9]{2,})(?=.*?[#?!@$%^&*-]{2,}).{8,}$", ErrorMessage = "Confirm Password Contains 3 Char, 2 number and 2 Special Char.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
