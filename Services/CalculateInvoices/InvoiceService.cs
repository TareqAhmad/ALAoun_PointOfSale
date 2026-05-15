using ALAoun_Pos.Models;

namespace ALAoun_Pos.Services.CalculateInvoices
{
    public class InvoiceService
    {
        public InvoiceResult CalculateInvoice(InvoiceDto dto)
        {
            var result = new InvoiceResult();
            
            foreach (var item in dto.Items)
            {
                decimal lineSubTotal = Math.Round(item.Quantity * item.UnitPrice, 3);

                decimal lineDiscountAmount = Math.Round(lineSubTotal * (item.DiscountPercent / 100), 3);

                decimal taxableAmount = lineSubTotal - lineDiscountAmount;

                decimal lineTaxAmount = Math.Round(taxableAmount * (item.TaxPercent / 100), 3);

                decimal lineTotal = taxableAmount + lineTaxAmount;

                result.TotalSubtotal += lineSubTotal;
                result.TotalDiscount += lineDiscountAmount;
                result.TotalTaxAmount += lineTaxAmount;
                result.TotalTaxableAmount += taxableAmount; 
                result.FinalGrandTotal += lineTotal;     

                result.CalculatedItems.Add(new CalculatedInvoiceItem
                {
                    ProductId = item.ProductId,
                    LineSubtotal = lineSubTotal,
                    LineDiscountAmount = lineDiscountAmount,
                    LineTaxAmount = lineTaxAmount,
                    LineTotal = lineTotal
                });
            }

            return result;
        }
    }
}