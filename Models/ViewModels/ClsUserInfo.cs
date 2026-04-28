namespace ALAoun_Pos.Models
{
 public class ClsUserInfo
{ 
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int PosId { get; set; }
        public int UserId { get; set; }
        public int RoleId {get; set;}
        public string? UserName {get; set;}
        public string? CompanyName {get; set; }
        public string? BranchName {get; set; }
        public string? PosName {get; set;}
 }
}