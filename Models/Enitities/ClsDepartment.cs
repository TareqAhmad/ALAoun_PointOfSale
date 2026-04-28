using System;

namespace ALAoun_Pos.Models
{
    public class ClsDepartments
    {
        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public int CompanyId { get; set; }

        public int? BranchId { get; set; }

        // Navigation Properties
        public ClsCompanies? Company { get; set; }

        public ClsBranches? Branch { get; set; }
    }
}