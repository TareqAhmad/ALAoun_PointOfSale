
namespace ALAoun_Pos.Models
{
    public class ClsPosPoints
    {
        public int PosId { get; set; }
        public string? PosName { get; set; }
        public string? PosCode {get; set;}
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
         public int BranchId { get; set; }
    }
}