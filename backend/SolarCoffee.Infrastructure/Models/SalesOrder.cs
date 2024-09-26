namespace SolarCoffee.Infrastructure.Models
{
    public class SalesOrder : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }  
        public Customer Customer { get; set; }      
        public List<SalesOrderItem> SalesOrderItems { get; set; }
        public bool IsPaid { get; set; }
    }
}