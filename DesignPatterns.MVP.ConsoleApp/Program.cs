using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns.MVP.ConsoleApp;

namespace DesignPatterns.MVP.ConsoleApp
{
    static class Program
    {
        public static void Main()
        {
            IProductProvider productProvider = new ProductProvider();
            IView view = new ConsoleView();

            IPresenter presenter = new Presenter(view, productProvider);
            presenter.ShowView();
        }
    }
}
