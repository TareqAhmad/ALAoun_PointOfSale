namespace ALAoun_Pos.Services
{
    public class PricingService
{
    public decimal CalculateItemSubTotal(decimal price, int quantity)
    {
        return price * quantity;
    }
    

    public decimal CalculateDiscount(decimal amount, decimal Percentage )
    {
        return amount * (Percentage / 100);
    } 

    public decimal CalculateTax(decimal amount, decimal Percentage)
    {
        return amount * (Percentage / 100);
    }

    public decimal CalculateItemNet(decimal price, int quantity, decimal discount, decimal tax)
    {
        var total = CalculateItemSubTotal(price,quantity);
        return total - discount + tax;
    }
}
}