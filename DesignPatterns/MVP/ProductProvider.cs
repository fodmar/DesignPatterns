using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.MVP
{
    public class ProductProvider : IProductProvider
    {
        public IEnumerable<Product> GetProducts()
        {
            return new ProductDetails[]
            {
                new ProductDetails
                {
                    Name = "Plate",
                    Category = "Kitchen",
                    Manufacturer = "Unknown",
                    Description = "Big plate",
                    Price = 100.1m
                },
                new ProductDetails
                {
                    Name = "Flowers",
                    Category = "Garden",
                    Manufacturer = "Nature",
                    Description = "Beautiful, yellow flowers",
                    Price = 66.05m
                },
            };
        }

        public ProductDetails GetProductDetails(Product product)
        {
            return product as ProductDetails;
        }
    }
}
