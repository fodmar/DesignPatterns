using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window, IView
    {
        private ProductDetails currentDetails;

        public MainWindow()
        {
            InitializeComponent();
            this.spProducts.ItemsSource = new ProductProvider().GetProducts();  
        }

        public void Show(IEnumerable<Product> products)
        {
            this.Show();
            this.spProducts.ItemsSource = products;

            DetailsRequested(null, new ProductEventArgs { Product = products.First() });
        }

        public void ShowDetails(ProductDetails productDetails)
        {
            this.tbName.Text = productDetails.Name;
            this.tbCategory.Text = productDetails.Category;
            this.tbManufacturer.Text = productDetails.Manufacturer;
            this.tbPrice.Text = productDetails.Price.ToString();
            this.tbDescription.Text = productDetails.Description;

            this.currentDetails = productDetails;
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
            OrderRequested(null, new ProductEventArgs { Product = this.currentDetails });
        }
    }
}
