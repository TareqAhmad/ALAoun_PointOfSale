

namespace ALAoun_Pos.Models
{
public class PendingInvoiceViewModel
{
    public int PendingInvoiceId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public int CustomerId { get; set; }

    public int QuantityItems { get; set; }

    public string? InvoiceType { get; set; }
    public string? PaymentMethod { get; set; }

    // مهم: Items جاية كـ JSON
    public List<CartItemsViewModel>? Items { get; set; }
}
}