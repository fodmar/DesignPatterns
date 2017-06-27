using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.MVP
{
    public class Presenter : IPresenter
    {
        private IView view;
        private IProductProvider productProvider;

        public Presenter(IView view, IProductProvider productProvider)
        {
            this.productProvider = productProvider;
            this.view = view;

            this.view.DetailsRequested += view_DetailsRequested;
            this.view.OrderRequested += view_OrderRequested;
            this.view.ExitRequested += view_ExitRequested;
        }

        public void ShowView()
        {
            IEnumerable<Product> products = this.productProvider.GetProducts();

            this.view.Show(products);
        }

        private void view_DetailsRequested(object sender, ProductEventArgs e)
        {
            ProductDetails details = this.productProvider.GetProductDetails(e.Product);

            this.view.ShowDetails(details);
        }

        void view_OrderRequested(object sender, ProductEventArgs e)
        {
            if (this.view.Confirm("Do you really want to make an order?"))
            {
                this.view.Inform("Order has been made");
            }

            this.ShowView();
        }

        void view_ExitRequested(object sender, EventArgs e)
        {
            if (this.view.Confirm("Do you really want to exit?"))
            {
                this.view.Hide();
            }
            else
            {
                this.ShowView();
            }
        }
    }
}
