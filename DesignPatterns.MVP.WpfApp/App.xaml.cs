using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DesignPatterns.MVP.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IView view = new MainWindow();
            IProductProvider productProvider = new ProductProvider();

            IPresenter presenter = new Presenter(view, productProvider);
            presenter.ShowView();
        }
    }
}
