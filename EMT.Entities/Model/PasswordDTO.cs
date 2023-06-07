using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMT.Entities.Model
{
    public class PasswordDTO
    {
        [Required(ErrorMessage = "Please Fill Password")]
        [MinLength(8, ErrorMessage = "Password Minimum 8 Char")]
        [RegularExpression("^(?=.*?[A-Za-z]{3,})(?=.*?[0-9]{2,})(?=.*?[#?!@$%^&*-]{2,}).{8,}$", ErrorMessage = "Password Contains 3 Char, 2 number and 2 Special Char.")]
        public string Password { get; set; } = null!;
        [NotMapped]
        [Required(ErrorMessage = "Please Fill Confirm Password")]
        [MinLength(8, ErrorMessage = "Password Minimum 8 Char")]
        [RegularExpression("^(?=.*?[A-Za-z]{3,})(?=.*?[0-9]{2,})(?=.*?[#?!@$%^&*-]{2,}).{8,}$", ErrorMessage = "Confirm Password Contains 3 Char, 2 number and 2 Special Char.")]
        public string ConfirmPassword { get; set; } = null!;
        [NotMapped]
        [Compare("ConfirmPassword")]
        [Required(ErrorMessage = "Please Fill Confirm New Password")]
        [MinLength(8, ErrorMessage = "Password Minimum 8 Char")]
        [RegularExpression("^(?=.*?[A-Za-z]{3,})(?=.*?[0-9]{2,})(?=.*?[#?!@$%^&*-]{2,}).{8,}$", ErrorMessage = "Confirm New Password Contains 3 Char, 2 number and 2 Special Char.")]
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
