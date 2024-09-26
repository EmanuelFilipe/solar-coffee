namespace SolarCoffee.Infrastructure.Models
{
    public class Customer : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerAddress PrimaryAddress {get; set;}
    }
}