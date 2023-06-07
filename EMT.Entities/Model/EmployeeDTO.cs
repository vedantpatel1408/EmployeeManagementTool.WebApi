using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMT.Entities.Model
{
    public class EmployeeDTO
    {
        public long EmpId { get; set; }

        public string Firstname { get; set; } = null!;

        public string? Lastname { get; set; }

        public string Email { get; set; } = null!;

        public DateTime? Dob { get; set; }

        public string? Gender { get; set; }

        public string Password { get; set; } = null!;
        public int? Attempts { get; set; }

        public int TotalAttempts { get; set; }

        public bool? Status { get; set; }

        public bool? IsLocked { get; set; }
    }
}
