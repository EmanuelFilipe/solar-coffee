using Microsoft.EntityFrameworkCore;
using SolarCoffee.Infrastructure.Models;
using SolarCoffee.Infrastructure.Persistence;

namespace SolarCoffee.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _context;

        public ProductService(SolarDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all Product from the database
        /// </summary>
        /// <returns></returns>
        public List<Infrastructure.Models.Product> GetAll()
        {
            return _context.Products.ToList();
        }

        /// <summary>
        /// Retrieves a Product from the database by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Infrastructure.Models.Product GetById(int id)
        {
            return _context.Products.Where(p => p.Id == id)
                                    .FirstOrDefault();
        }
        
        /// <summary>
        /// Adss new product to the database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ServiceResponse<Infrastructure.Models.Product> Create(Infrastructure.Models.Product product)
        {
            try
            {
                _context.Products.Add(product);

                var newInventory = new ProductInventory {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _context.ProductInventories.Add(newInventory);
                _context.SaveChanges();

                return new ServiceResponse<Infrastructure.Models.Product> {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Save new product",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Infrastructure.Models.Product> {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = $"Message: {ex.Message} \n StackTrace: {ex.StackTrace}",
                    IsSuccess = false
                };
            }

        }
        
        /// <summary>
        /// Archives a Product by setting boolean IsArchived to true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResponse<Infrastructure.Models.Product> Archive(int id)
        {
            try {
                var product = GetById(id);
                product.IsArchived = true;

                _context.Update(product);
                _context.SaveChanges();

                return new ServiceResponse<Infrastructure.Models.Product> {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Archived product",
                    IsSuccess = true
                };
            }
            catch (Exception ex) {
                return new ServiceResponse<Infrastructure.Models.Product> {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = $"Message: {ex.Message} \n StackTrace: {ex.StackTrace}",
                    IsSuccess = false
                };
            }
        }
    }
}