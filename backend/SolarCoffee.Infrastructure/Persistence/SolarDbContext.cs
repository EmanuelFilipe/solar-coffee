using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolarCoffee.Infrastructure.Models;

namespace SolarCoffee.Infrastructure.Persistence
{
    public class SolarDbContext : IdentityDbContext
    {
        public SolarDbContext()
        { }

        public SolarDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set;}
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set;}
    }
}