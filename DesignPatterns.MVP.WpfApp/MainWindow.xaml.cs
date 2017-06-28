using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignPatterns.MVP.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView, INotifyPropertyChanged
    {
        public ProductDetails CurrentDetails { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Show(IEnumerable<Product> products)
        {
            Product[] productsArray = products.ToArray();

            this.spProducts.ItemsSource = productsArray;

            if (productsArray.Any())
            {
                DetailsRequested(null, new ProductEventArgs { Product = productsArray.First() });
            }

            this.Show();
        }

        public void ShowDetails(ProductDetails productDetails)
        {
            this.CurrentDetails = productDetails;

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("CurrentDetails"));
            }
        }

        public void Inform(string message)
        {
            MessageBox.Show(message);
        }

        public bool Confirm(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Confirmation", MessageBoxButton.YesNo);

            return result == MessageBoxResult.Yes;
        }

        public event EventHandler<ProductEventArgs> DetailsRequested;

        public event EventHandler<ProductEventArgs> OrderRequested;

        public event EventHandler ExitRequested;

        public event PropertyChangedEventHandler PropertyChanged;

        private void spProducts_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListBox).SelectedItem as Product;

            if (item != null)
            {
                DetailsRequested(null, new ProductEventArgs { Product = item });
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderRequested(null, new ProductEventArgs { Product = this.CurrentDetails });
        }
    }
}
