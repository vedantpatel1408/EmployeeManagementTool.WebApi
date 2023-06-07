using System.ComponentModel.DataAnnotations;

namespace EMT.Entities.Model
{
    public class CreateEmployeeModel
    {
        [Required]
        public string Firstname { get; set; } = null!;

        public string? Lastname { get; set; }
        [Required]
        public string Email { get; set; } = null!;

        public DateTime? Dob { get; set; }

        public string? Gender { get; set; }
        [Required]
        public string Password { get; set; } = null!;
    }
}
