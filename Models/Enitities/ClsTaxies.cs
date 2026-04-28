
namespace ALAoun_Pos.Models
{
    public class ClsTaxies
    {
        public int taxId { get; set; }
        public string? taxName { get; set; }
        public decimal taxRate { get; set; }
        public string? description { get; set; }
        public int CompanyId { get; set; }
  
    }
}