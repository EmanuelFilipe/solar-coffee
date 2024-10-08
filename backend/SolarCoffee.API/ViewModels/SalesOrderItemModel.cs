namespace SolarCoffee.API.ViewModels
{
    /// <summary>
    /// ViewModel for SalesOrderItem
    /// </summary>
    public class SalesOrderItemModel {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductModel Product { get; set; }
    }
}