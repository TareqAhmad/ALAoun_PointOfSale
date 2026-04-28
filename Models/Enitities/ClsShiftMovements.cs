using System;

namespace ALAoun_Pos.Models
{
    public class ClsShiftMovements
    {
        public int MovementId { get; set; }

        public decimal? OpeningCash { get; set; }

        public decimal? CashSales { get; set; }

        public decimal? CashReturnSales { get; set; }

        public int ShiftId { get; set; }

        public int CompanyId { get; set; }

        public int? BranchId { get; set; }


        // Navigation Properties
        public ClsPosShifts? Shift { get; set; }

        public ClsCompanies? Company { get; set; }

        public ClsBranches? Branch { get; set; }
    }
}