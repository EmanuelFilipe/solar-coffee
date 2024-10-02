using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SolarCoffee.API.ViewModels;

namespace SolarCoffee.API.Serialization
{
    public class ProductMapper
    {
        /// <summary>
        /// Maps a Product data model to a ProductModel viewmodel
        /// </summary>
        /// <param name="product"></param>
        /// <returns>return a new object ProductModel filled</returns>
        public static ProductModel SerializeProductModel(Infrastructure.Models.Product product) {
            return new ProductModel {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }

        /// <summary>
        /// Maps a ProductModel viewModel to a Product data model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static Infrastructure.Models.Product SerializeProductModel(ProductModel product) {
            return new Infrastructure.Models.Product {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsTaxable = product.IsTaxable,
                IsArchived = product.IsArchived
            };
        }
    }
}