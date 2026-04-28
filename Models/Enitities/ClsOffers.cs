

namespace ALAoun_Pos.Models
{
    public class ClsOffers
    {
        public int OfferId { get; set; }
        public string? OfferName { get; set; }
        public string? OfferType { get; set; }
        public decimal OfferRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }



    }
}