using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.MVP
{
    public interface IProductProvider
    {
        IEnumerable<Product> GetProducts();

        ProductDetails GetProductDetails(Product product);
    }
}
