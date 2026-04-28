using System;

namespace ALAoun_Pos.Models
{
    public class ClsPosShifts
    {
        public int ShiftId { get; set; }

        public string? ShiftName { get; set; }

        public DateTime ShiftDate { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? WorkStationId { get; set; }

        public int? PosId { get; set; }

        public int CompanyId { get; set; }

        public int? BranchId { get; set; }


        // Navigation Properties
        public ClsPosWorkStations? WorkStation { get; set; }

        public ClsPosPoints? Pos { get; set; }

        public ClsCompanies? Company { get; set; }

        public ClsBranches? Branch { get; set; }
    }
}