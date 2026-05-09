

namespace ALAoun_Pos.Models
{
    public class AuditLoginDto
    {
        public int userId {get; set;}
        public string userName {get;set; } = null; 
        public DateTime logTime {get; set; }
        public int logTypeId {get; set; }
        public DateTime shiftDate {get; set; }
        public string? notes {get; set;}
        public int companyId {get;set; } 
        public int branchId {get;set; } 
        public int posId  {get;set; } 


    }
}

