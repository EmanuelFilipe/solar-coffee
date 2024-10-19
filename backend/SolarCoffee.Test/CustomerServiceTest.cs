using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SolarCoffee.Infrastructure.Models;
using SolarCoffee.Infrastructure.Persistence;
using SolarCoffee.Services.Customer;

namespace SolarCoffee.Test;

public class CustomerServiceTest
{
    [Fact]
    public void GetsAllCustomers_GivenThenExist()
    {
        var options = new DbContextOptionsBuilder<SolarDbContext>()
                            .UseInMemoryDatabase("gets_all").Options;

        using var context = new SolarDbContext(options);

        var service = new CustomerService(context);

        service.Create(new Customer { Id = 123456 });
        service.Create(new Customer { Id = -155 });

        var allCustomers = service.GetAll();
        allCustomers.Count.Should().Be(2);
    }

    [Fact]
    public void CreateCustomer_GivenNewCustomerObject()
    {
        var options = new DbContextOptionsBuilder<SolarDbContext>()
                            .UseInMemoryDatabase("Add_writes_to_database").Options;

        using var context = new SolarDbContext(options);
        var service = new CustomerService(context);

        service.Create(new Customer { Id = 12 });
        context.Customers.Single().Id.Should().Be(12);
    }

    [Fact]
    public void DeleteCustomer_GivenId()
    {
        var options = new DbContextOptionsBuilder<SolarDbContext>()
                            .UseInMemoryDatabase("deletes_one").Options;

        using var context = new SolarDbContext(options);
        var service = new CustomerService(context);

        service.Create(new Customer { Id = 12 });
        service.Delete(12);
        var allCustomers = service.GetAll();
        allCustomers.Count.Should().Be(0);
    }

    [Fact]
    public void OrdersByLastName_WhenGetAllCustomersInvoked()
    {
        // arrange
        var data = new List<Customer>
        {
            new Customer { Id = 123, LastName = "Zulu" },
            new Customer { Id = 323, LastName = "Alpha" },
            new Customer { Id = -89, LastName = "Xu" }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Customer>>();

        mockSet.As<IQueryable<Customer>>()
            .Setup(m => m.Provider)
            .Returns(data.Provider);

        mockSet.As<IQueryable<Customer>>()
            .Setup(m => m.Expression)
            .Returns(data.Expression);

        mockSet.As<IQueryable<Customer>>()
            .Setup(m => m.ElementType)
            .Returns(data.ElementType);

        mockSet.As<IQueryable<Customer>>()
            .Setup(m => m.GetEnumerator())
            .Returns(data.GetEnumerator());

        var mockContext = new Mock<SolarDbContext>();

        mockContext.Setup(c => c.Customers)
                   .Returns(mockSet.Object); 

        // act
        var service = new CustomerService(mockContext.Object);
        var customers = service.GetAll();

        // assert
        customers.Count.Should().Be(3);
        customers[0].Id.Should().Be(323);
        customers[1].Id.Should().Be(-89);
        customers[2].Id.Should().Be(123);
    }
}