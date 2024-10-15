using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Infrastructure.Models;
using SolarCoffee.Infrastructure.Persistence;

namespace SolarCoffee.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _context;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolarDbContext context, 
                                ILogger<InventoryService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Gets a ProductInventory instance by Product ID
        /// </summary>
        /// <param name="id">productId</param>
        /// <returns>ProductInventory</returns>
        public ProductInventory GetById(int id)
        {
            return _context.ProductInventories.Include(pi => pi.Product)
                                              .FirstOrDefault(pi => pi.Product.Id == id);
        }

        /// <summary>
        /// Returns all current inventory from the database
        /// </summary>
        /// <returns></returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _context.ProductInventories.Include(pi => pi.Product)
                                              .Where(pi => !pi.Product.IsArchived)
                                              .ToList();
        }

        /// <summary>
        /// Return Snapshot history for the previous 6 hours
        /// </summary>
        /// <returns>List<ProductInventorySnapshot></returns>
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow - TimeSpan.FromHours(6);

            return _context.ProductInventorySnapshots
                           .Include(snap => snap.Product)
                           .Where(snap => snap.SnapshotTime > earliest && !snap.Product.IsArchived)
                           .ToList();
        }

        /// <summary>
        /// Creates a Snapshot record using the provided ProductInventory instance
        /// </summary>
        /// <param name="inventory"></param>
        private void CreateSnapshot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot {
                SnapshotTime = DateTime.UtcNow,
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand
            };
            // TODO - ver se vai dar erro
            _context.Add(snapshot);
        }

        /// <summary>
        /// Updates number of units available of the provided product id
        /// Adjusts QuantityOnHand by adjustment value
        /// </summary>
        /// <param name="id">productId</param>
        /// <param name="adjustment">number of units added</param>
        /// <returns>ServiceResponse<ProductInventory></returns>
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment)
        {
            try
            {
                var inventory = _context.ProductInventories
                    .Include(pi => pi.Product)
                    .First(pi => pi.Product.Id == id);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot(inventory);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error creating inventory snapshot.");
                    _logger.LogError(ex.StackTrace);

                }
                _context.SaveChanges();

                return new ServiceResponse<ProductInventory> {
                    Data = inventory,
                    Time = DateTime.UtcNow,
                    Message = $"Product {id} inventory adjusted",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ProductInventory> {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = $"Message: {ex.Message} \n StackTrace: {ex.StackTrace}",
                    IsSuccess = false
                };
            }
        }
    }
}