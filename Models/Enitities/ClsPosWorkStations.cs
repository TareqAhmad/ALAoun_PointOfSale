

namespace ALAoun_Pos.Models
{
    public class ClsPosWorkStations 
    {
        public int WorkStationId { get; set; }
        public string? WorkStationName { get; set; }
        public string? WorkStationCode { get; set; }
        public string? WorkStationLocation { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int PosId { get; set; }

    }
    
}   