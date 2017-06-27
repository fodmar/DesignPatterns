using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.MVP
{
    public interface IView
    {
        void Show(IEnumerable<Product> products);

        void ShowDetails(ProductDetails productDetails);

        void Inform(string message);

        bool Confirm(string message);

        void Hide();

        event EventHandler<ProductEventArgs> DetailsRequested;

        event EventHandler<ProductEventArgs> OrderRequested;

        event EventHandler ExitRequested;
    }
}
