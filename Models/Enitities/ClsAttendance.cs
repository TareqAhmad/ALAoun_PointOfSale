using System;

namespace ALAoun_Pos.Models
{
    public class ClsAttendance
    {
        public int AttendanceId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime ClockIn { get; set; }

        public DateTime? ClockOut { get; set; }

        public decimal? WorkHours { get; set; }

        public DateTime? ShiftDate { get; set; }

        public string? Notes { get; set; }

        public int CompanyId { get; set; }

        public int? BranchId { get; set; }


        // Navigation Properties
        public ClsEmployees? Employee { get; set; }

        public ClsCompanies? Company { get; set; }

        public ClsBranches? Branch { get; set; }
    }
}