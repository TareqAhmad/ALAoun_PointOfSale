using System;
using System.Collections.Generic;

namespace ALAoun_Pos.Models
{
    public class ClsPendingInvoices
    {

        public bool Status {get; set;}
        public int PendingInvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal NetAmount { get; set; }

        public int? DiscountId { get; set; }

        public int? TaxId { get; set; }

        public int QuantityItems { get; set; }

        public int? InvoiceTypeId { get; set; }

        public int? PaymentId { get; set; }

        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public int UserId { get; set; }

        public int PosId { get; set; }

        public string? Notes { get; set; }


    }
}