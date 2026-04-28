namespace ALAoun_Pos.Models
{
 public class ClsLogin
 { 
        public int UserId { get; set; }
        public string? UserName {get; set;}
        public string? UserPassword {get; set;}
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int PosId { get; set; }

 }
}