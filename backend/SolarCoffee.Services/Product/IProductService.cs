using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SolarCoffee.Services.Product
{
    public interface IProductService
    {
        List<Infrastructure.Models.Product> GetAll();
        Infrastructure.Models.Product GetById(int id);
        ServiceResponse<Infrastructure.Models.Product> Create(Infrastructure.Models.Product product);
        ServiceResponse<Infrastructure.Models.Product> Archive(int id);
    }
}