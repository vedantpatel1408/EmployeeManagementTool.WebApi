using System;
using System.Collections.Generic;

namespace EMT.Entities.DataModels;

public partial class Admin
{
    public long AdminId { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Lastname { get; set; }

    public string Email { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string? Gender { get; set; }

    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? Attempts { get; set; }

    public int TotalAttempts { get; set; }

    public bool? Status { get; set; }

    public bool? IsLocked { get; set; }
}
