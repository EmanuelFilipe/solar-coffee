namespace SolarCoffee.Infrastructure.Models
{
    public class SalesOrderItem : BaseEntity
    {
        public int Quantity { get; set; }
        public Product Product { get; set; }
    }
}