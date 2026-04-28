using System;

namespace ALAoun_Pos.Models
{
    public class ClsEmployees
    {
        public int EmployeeId { get; set; }

        public string? EmployeeName { get; set; }

        public string? Position { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public DateTime HireDate { get; set; }

        public decimal? Salary { get; set; }

        public int? UserId { get; set; }

        public int? DepartmentId { get; set; }

        public int CompanyId { get; set; }

        public int? BranchId { get; set; }


        // Navigation Properties
        public ClsUsers? User { get; set; }

        public ClsDepartments? Department { get; set; }

        public ClsCompanies? Company { get; set; }

        public ClsBranches? Branch { get; set; }
    }
}