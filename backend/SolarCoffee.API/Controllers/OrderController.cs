using Microsoft.AspNetCore.Mvc;
using SolarCoffee.API.Serialization;
using SolarCoffee.API.ViewModels;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Order;

namespace SolarCoffee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(ILogger<OrderController> logger, 
                               IOrderService orderService, 
                               ICustomerService customerService)
        {
            _logger = logger;
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost("/invoice")]
        public IActionResult GenerateNewOrder([FromBody] InvoiceModel invoice)
        {
            _logger.LogInformation("Generating invoice");
            var order = OrderMapper.SerializeInvoiceToOrder(invoice);
            order.Customer = _customerService.GetById(invoice.CustomerId);
            _orderService.GenerateOpenOrder(order);
            return Ok(order);
        }
        
    }
}