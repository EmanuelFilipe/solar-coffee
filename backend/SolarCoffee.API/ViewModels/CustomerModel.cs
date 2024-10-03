using System.ComponentModel.DataAnnotations;

namespace SolarCoffee.API.ViewModels
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public CustomerAddressModel PrimaryAddress { get; set; }
    }
}