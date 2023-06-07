using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementTool.DataModels;

public partial class Admin
{
    public long AdminId { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Lastname { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string? Gender { get; set; }

    [Required(ErrorMessage = "Please Fill Password")]
    [MinLength(8, ErrorMessage = "Password Minimum 8 Char")]
    [RegularExpression("^(?=.*?[A-Za-z]{3,})(?=.*?[0-9]{2,})(?=.*?[#?!@$%^&*-]{2,}).{8,}$", ErrorMessage = "Password Contains 3 Char, 2 number and 2 Special Char.")]
    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? Attempts { get; set; }

    public int TotalAttempts { get; set; }

    public bool? Status { get; set; }

    public bool? IsLocked { get; set; }
}
