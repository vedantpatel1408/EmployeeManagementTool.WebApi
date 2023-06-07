using System;
using System.Collections.Generic;

namespace EmployeeManagementTool.DataModels;

public partial class PasswordExpiry
{
    public long ResetId { get; set; }

    public long EmpId { get; set; }

    public int PasswordexpiryDay { get; set; }

    public bool PasswordexpiryStatus { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? PasswordUpdated { get; set; }

    public virtual Employee Emp { get; set; } = null!;
}
