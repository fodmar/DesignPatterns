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
                    Name = "A",
                    Category = "A",
                    Manufacturer = "A",
                    Description = "A",
                    Price = 100m
                },
                new ProductDetails
                {
                    Name = "B",
                    Category = "B",
                    Manufacturer = "B",
                    Description = "B",
                    Price = 100m
                },
            };
        }

        public ProductDetails GetProductDetails(Product product)
        {
            return product as ProductDetails;
        }
    }
}
