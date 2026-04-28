using System;

namespace ALAoun_Pos.Models
{
    public class ClsPendingInvoiceItems
    {
        public int ItemId { get; set; }

       public int PendingInvoiceId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public int? DiscountId { get; set; }

        public int? TaxId { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; }

        // Navigation
        // public ClsPendingInvoices? PendingInvoice { get; set; }
    }
}