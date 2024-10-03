using SolarCoffee.API.ViewModels;
using SolarCoffee.Infrastructure.Models;

namespace SolarCoffee.API.Serialization
{
    public class CustomerMapper
    {
        /// <summary>
        /// Serializes a Customer data model into a CustomerModel view model
        /// </summary>
        /// <param name="customer"></param>
        public static CustomerModel SerializeCustomer(Customer customer) 
        {
            // var address = new CustomerAddressModel 
            // {
            //     Id = customer.Id,
            //     AddressLine1 = customer.PrimaryAddress.AddressLine1,
            //     AddressLine2 = customer.PrimaryAddress.AddressLine2,
            //     City = customer.PrimaryAddress.City,
            //     State = customer.PrimaryAddress.State,
            //     Country = customer.PrimaryAddress.Country,
            //     PostalCode = customer.PrimaryAddress.PostalCode,
            //     CreatedOn = customer.PrimaryAddress.CreatedOn,
            //     UpdatedOn = customer.PrimaryAddress.UpdatedOn
            // };

            return new CustomerModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

        /// <summary>
        /// Serializes a CustomerModel  into a CustomerModel data model
        /// </summary>
        /// <param name="customer"></param>
        public static Customer SerializeCustomer(CustomerModel customer) 
        {
            return new Customer
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

        /// <summary>
        /// Maps a CustomerAddress data model to CustomerAddressModel view model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static CustomerAddressModel MapCustomerAddress (CustomerAddress address)
        {
            return new CustomerAddressModel 
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }

        /// <summary>
        /// Maps a CustomerAddressModel view model to CustomerAddress data model
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static CustomerAddress MapCustomerAddress (CustomerAddressModel address)
        {
            return new CustomerAddress
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                PostalCode = address.PostalCode,
                Country = address.Country,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
    }
}