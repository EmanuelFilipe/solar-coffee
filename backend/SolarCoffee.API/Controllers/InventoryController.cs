using Microsoft.AspNetCore.Mvc;
using SolarCoffee.API.Serialization;
using SolarCoffee.API.ViewModels;
using SolarCoffee.Infrastructure.Models;
using SolarCoffee.Services.Inventory;

namespace SolarCoffee.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IInventoryService _inventoryService;

        public InventoryController(ILogger<CustomerController> logger, 
                                   IInventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public IActionResult GetCurrentInventory()
        {
            _logger.LogInformation("Getting all inventoty...");

            var inventoty = _inventoryService.GetCurrentInventory()
                .Select(pi => new ProductInventoryModel {
                    Id = pi.Id,
                    QuantityOnHand = pi.QuantityOnHand,
                    IdealQuantity = pi.IdealQuantity,
                    Product = ProductMapper.SerializeProductModel(pi.Product),
                })
                .OrderBy(inv => inv.Product.Name)
                .ToList();
            return Ok(inventoty);
        }

        [HttpPatch]
        public IActionResult UpdateInventory([FromBody] ShipmentModel shipment)
        {
            if (!ModelState.IsValid)
               return BadRequest(ModelState);

            _logger.LogInformation($"Updating inventoty \n " +
                                   $"for {shipment.ProductId} - Adjustment: {shipment.Adjustment}"); 
            
            var id = shipment.ProductId;
            var adjustment = shipment.Adjustment;
            var inventory = _inventoryService.UpdateUnitsAvailable(id, adjustment);

            return Ok(inventory);
        }

        [HttpGet("snapshot")]
        public IActionResult GetSnapshotHistory()
        {
            _logger.LogInformation("Getting snaptshot history...");

            try
            {
                var snapshotHistory = _inventoryService.GetSnapshotHistory();

                //get distinct points in time a snapshot was collected
                var timelineMarkers = snapshotHistory
                    .Select(t => t.SnapshotTime)
                    .Distinct()
                    .ToList();

                // get quantities grouped by id
                var snapshots = snapshotHistory
                    .GroupBy(hist => hist.Product, hist => hist.QuantityOnHand,
                    (key, g) => new ProductInventorySnapshotModel
                    {
                        ProductId = key.Id,
                        QuantityOnHand = g.ToList()
                    })
                    .OrderBy(hist => hist.ProductId)
                    .ToList();

                var snapshotViewModel = new SnapshotResponse
                {
                    ProductInventorySnapshots = snapshots,
                    Timeline = timelineMarkers
                };

                return Ok(snapshotViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("error getting snapshot history.");
                _logger.LogError(ex.StackTrace);
                return BadRequest("Error retrieving history");
            }
        }

    }
}
