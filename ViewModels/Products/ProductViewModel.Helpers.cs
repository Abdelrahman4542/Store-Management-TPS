using StoreManagementSystem.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace StoreManagementSystem.ViewModels
{
    public partial class ProductViewModel
    {
        private void FilterProducts()
        {
            Products.Clear();

            var filteredProducts =
                string.IsNullOrWhiteSpace(SearchText)
                ? AllProducts
                : new ObservableCollection<Product>(
                    AllProducts.Where(p =>
                        p.Name.Contains(
                            SearchText,
                            System.StringComparison
                            .OrdinalIgnoreCase)));

            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }

            RefreshDashboard();
        }

        private void RefreshDashboard()
        {
            OnPropertyChanged(
                nameof(TotalProducts));

            OnPropertyChanged(
                nameof(LowStockCount));

            OnPropertyChanged(
                nameof(InventoryValue));
        }

        private void RefreshCart()
        {
            OnPropertyChanged(
                nameof(SubTotal));

            OnPropertyChanged(
                nameof(VAT));

            OnPropertyChanged(
                nameof(FinalTotal));
        }
    }
}