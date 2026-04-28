namespace ALAoun_Pos.Models
{
     
     public class UserRolePrivilegesVM
    {
        
        public int RoleId {get; set;}
        public int UserId { get; set; }

        public List<ClsPrivileges>? Privileges { get; set; }



    }
}
