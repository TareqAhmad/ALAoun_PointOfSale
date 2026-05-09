
namespace ALAoun_POS.Models
{

    public class CustomerDto
    {
        public string? customerName { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? address   { get; set; }
        public int companyId { get; set; }
        public int branchId { get; set; }
    }
}