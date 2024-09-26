namespace SolarCoffee.Infrastructure.Models
{
    public class ProductInventorySnapshot : BaseEntity
    {
        public DateTime SnapshotTime { get; set; }
        public Product Product { get; set; }
    }
}