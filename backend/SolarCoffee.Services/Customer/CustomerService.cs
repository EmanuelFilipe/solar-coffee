using Microsoft.EntityFrameworkCore;
using SolarCoffee.Infrastructure.Persistence;

namespace SolarCoffee.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDbContext _context;

        public CustomerService(SolarDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns a list of customers from the database
        /// </summary>
        /// <returns>List<Customer></returns>
        public List<Infrastructure.Models.Customer> GetAll()
        {
            return _context.Customers.Include(c => c.PrimaryAddress)
                                     .OrderBy(c => c.LastName)
                                     .ToList();
        }

        /// <summary>
        /// Gets a customers record by primary key
        /// </summary>
        /// <param name="id">int customer primary key</param>
        /// <returns>Customer</returns>
        public Infrastructure.Models.Customer GetById(int id)
        {
             return _context.Customers.AsNoTracking()
                                      .Where(p => p.Id == id)
                                      .FirstOrDefault();
        }

        /// <summary>
        /// Adds a new Customer record
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <returns>ServiceResponse<Customer></returns>
        public ServiceResponse<Infrastructure.Models.Customer> Create(Infrastructure.Models.Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                return new ServiceResponse<Infrastructure.Models.Customer> {
                    Data = customer,
                    Time = DateTime.UtcNow,
                    Message = "New Customer added",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Infrastructure.Models.Customer> {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = $"Message: {ex.Message} \n StackTrace: {ex.StackTrace}",
                    IsSuccess = false
                };
            }
        }

         
        /// <summary>
        /// Deletes a customer record
        /// </summary>
        /// <param name="id">int customer primary key</param>
        /// <returns>ServiceResponse<bool></returns>
        public ServiceResponse<bool> Delete(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer is null) 
            {
                return new ServiceResponse<bool> {
                    Data = false,
                    Time = DateTime.UtcNow,
                    Message = "Customer deleted!",
                    IsSuccess = false
                };
            }

            try
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();

                return new ServiceResponse<bool> {
                    Data = true,
                    Time = DateTime.UtcNow,
                    Message = "Customer deleted",
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
    }
}