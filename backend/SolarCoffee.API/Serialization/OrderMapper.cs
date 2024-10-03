using SolarCoffee.API.ViewModels;
using SolarCoffee.Infrastructure.Models;
using SolarCoffee.Services.Order;

namespace SolarCoffee.API.Serialization
{
    /// <summary>
    /// Handles mapping data models to and from related view models
    /// </summary>
    public static class OrderMapper
    {
        /// <summary>
        /// Maps an InvoiceModel view model to a SalesOrder data model
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static SalesOrder SerializeInvoiceToOrder(InvoiceModel invoice)
        {
            var SalesOrderItems = invoice.LineItems.Select(item => new SalesOrderItem 
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)
            }).ToList();

            return new SalesOrder {
                SalesOrderItems = SalesOrderItems,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Maps a collection of SaleOrders (data) to OrderModels (view models)
        /// </summary>
        /// <param name="orders"></param>
        /// <returns> List<OrderModel> </returns>
        public static List<OrderModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders) 
        {
            return orders.Select(order => new OrderModel 
            {
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                SalesOrderItems = SerializeSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList();
        }

        /// <summary>
        /// Maps a collection of SalesOrderItems (data) to SalesOrderItemModels (view model)
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns>List<SalesOrderItemModel></returns>
        private static List<SalesOrderItemModel> SerializeSalesOrderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel 
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)
            }).ToList();
        }
    }
}