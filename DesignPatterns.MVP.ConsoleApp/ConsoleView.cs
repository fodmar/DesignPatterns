using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.MVP.ConsoleApp
{
    public class ConsoleView : IView
    {
        private Product[] products;

        public void Show(IEnumerable<Product> products)
        {
            this.products = products.ToArray();

            Console.WriteLine("Products:");
            for (int i = 0; i < this.products.Length; i++)
            {
                Product product = this.products[i];

                Console.WriteLine("[{0}] {1} ({2}) - {3}", i, product.Name, product.Price, product.Category);
            }

            this.WaitForUserAction();
        }

        public void ShowDetails(ProductDetails productDetails)
        {
            Console.WriteLine("Name: {0}", productDetails.Name);
            Console.WriteLine("Category: {0}", productDetails.Category);
            Console.WriteLine("Manufacturer: {0}", productDetails.Manufacturer);
            Console.WriteLine("Description: {0}", productDetails.Description);
            Console.WriteLine("Price: {0}", productDetails.Price);

            this.WaitForUserAction();
        }

        public void Inform(string message)
        {
            Console.WriteLine(message);
        }

        public bool Confirm(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press enter to confirm");
            
            ConsoleKeyInfo pressed = Console.ReadKey();

            return pressed.Key == ConsoleKey.Enter;
        }

        public void Hide()
        {
        }

        private void WaitForUserAction()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("To order product type 'o' and product index (ex. o2)");
            Console.WriteLine("To view product details type 'd' and product index (ex. d2)");
            Console.WriteLine("To exit type 0");
            string userInput = Console.ReadLine();

            int productIndex;
            bool correctIndex = int.TryParse(userInput.Substring(1, userInput.Length - 1), out productIndex);

            if (correctIndex && productIndex < this.products.Length)
            {
                ProductEventArgs args = new ProductEventArgs { Product = this.products[productIndex] };

                if (userInput.StartsWith("o"))
	            {
                    OrderRequested(null, args);
	            }
                else if (userInput.StartsWith("d"))
	            {
                    DetailsRequested(null, args);
	            }
            }
            else if (userInput == "0")
            {
                ExitRequested(null, EventArgs.Empty);

                return;
            }

            WaitForUserAction();
        }

        public event EventHandler<ProductEventArgs> DetailsRequested;

        public event EventHandler<ProductEventArgs> OrderRequested;

        public event EventHandler ExitRequested;
    }
}
