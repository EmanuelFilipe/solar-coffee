namespace SolarCoffee.Services.Customer
{
    public interface ICustomerService
    {
        List<Infrastructure.Models.Customer> GetAll();
        Infrastructure.Models.Customer GetById(int id);
        ServiceResponse<Infrastructure.Models.Customer> Create(Infrastructure.Models.Customer customer);
        ServiceResponse<bool> Delete(int id);
    }
}