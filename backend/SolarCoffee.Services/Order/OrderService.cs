using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Infrastructure.Models;
using SolarCoffee.Infrastructure.Persistence;
using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _context;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;
       
        public OrderService(SolarDbContext context, ILogger<OrderService> logger, 
                            ProductService productService, IInventoryService inventoryService)
        {
            _context = context;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// Gets all SalesOrders in the system
        /// </summary>
        /// <returns>List<SalesOrder></returns>
        public List<SalesOrder> GetOrders()
        {
            return _context.SalesOrders.AsNoTracking()
                                       .Include(so => so.Customer)
                                            .ThenInclude(customer => customer.PrimaryAddress)
                                       .Include(so => so.SalesOrderItems)
                                            .ThenInclude(orderItems => orderItems.Product)
                                       .ToList();
        }

        /// <summary>
        /// Creates an open SalesOrder
        /// </summary>
        /// <param name="order"></param>
        /// <returns>ServiceResponse<bool> </returns>
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            _logger.LogInformation("Generating new order");

            foreach(var item in order.SalesOrderItems) {
                item.Product = _productService.GetById(item.Product.Id);
                item.Quantity = item.Quantity;

                var inventoryId = _inventoryService.GetById(item.Product.Id).Id;
                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _context.SalesOrders.Add(order);
                _context.SaveChanges();

                return new ServiceResponse<bool> {
                    Data = true,
                    Time = DateTime.UtcNow,
                    Message = "Open order created",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool> {
                    Data = false,
                    Time = DateTime.UtcNow,
                    Message = $"Message: {ex.Message} \n StackTrace: {ex.StackTrace}",
                    IsSuccess = false
                };
            }
        }

        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            throw new NotImplementedException();
        }
    }
}