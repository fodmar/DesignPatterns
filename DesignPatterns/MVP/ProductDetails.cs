using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.MVP
{
    public class ProductDetails : Product
    {
        public string Description { get; set; }

        public string Manufacturer { get; set; }
    }
}
