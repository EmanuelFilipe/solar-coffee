using SolarCoffee.Infrastructure.Models;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        public List<ProductInventory> GetCurrentInventory();
        public List<ProductInventorySnapshot> GetSnapshotHistory();
        public ProductInventory GetById(int id);
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
    }
}