using SolarCoffee.Infrastructure.Models;

namespace SolarCoffee.Services.Order
{
    public interface IOrderService
    {
        List<SalesOrder> GetOrders();
        ServiceResponse<bool> GenerateOpenOrder(SalesOrder order);
        ServiceResponse<bool> MarkFulfilled(int id);
    }
}