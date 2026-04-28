namespace ALAoun_Pos.Models
{
     public class SalesInvoiceDto  // DTO Data Transfer Object
    {


        public int customerId {get; set;}

        public int InvoiceTypeId {get; set;}
        public int PaymentId {get; set;}
        public List<SalesInvoiceItemsDto>? Items {get; set;}

        public int companyId {get; set;}

        public int branchId {get; set;}

        public int posId {get; set;} 

        public int UserId {get; set;}

    }
}