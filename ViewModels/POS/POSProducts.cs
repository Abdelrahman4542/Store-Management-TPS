using StoreManagementSystem.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace StoreManagementSystem.ViewModels
{
    public partial class POSViewModel
    {
        private async Task LoadProductsAsync()
        {
            Products.Clear();

            AllProducts.Clear();

            var products =
                await _productRepository
                .GetAllProductsAsync();

            foreach (var product
                in products)
            {
                Products.Add(product);

                AllProducts.Add(product);
            }
        }

        private void FilterProducts()
        {
            Products.Clear();

            var filteredProducts =
                string.IsNullOrWhiteSpace(
                    SearchText)
                ? AllProducts
                : new ObservableCollection<Product>(
                    AllProducts.Where(p =>
                        p.Name.Contains(
                            SearchText,
                            System.StringComparison
                            .OrdinalIgnoreCase)));

            foreach (var product
                in filteredProducts)
            {
                Products.Add(product);
            }
        }
    }
}