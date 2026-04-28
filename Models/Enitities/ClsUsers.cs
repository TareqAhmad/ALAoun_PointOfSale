

namespace ALAoun_Pos.Models
{
    public class ClsUsers
    {
         public int UserId { get; set; }
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public int RoleId { get; set; }

        public int CompanyId { get; set; }

        public int BranchId { get; set; }   

        public int PosId { get; set; }
    }
}