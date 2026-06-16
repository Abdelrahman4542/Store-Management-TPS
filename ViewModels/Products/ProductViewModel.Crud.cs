using StoreManagementSystem.Models;
using System.Windows;

namespace StoreManagementSystem.ViewModels
{
    public partial class ProductViewModel
    {
        private async Task LoadProductsAsync()
        {
            Products.Clear();

            AllProducts.Clear();

            var products =
                await _productRepository
                .GetAllProductsAsync();

            foreach (var product in products)
            {
                Products.Add(product);

                AllProducts.Add(product);
            }

            RefreshDashboard();
        }

        private async Task AddProductAsync()
        {
            if (!ValidateProduct())
                return;

            await _productRepository
                .AddProductAsync(SelectedProduct);

            await LoadProductsAsync();

            SelectedProduct = new Product();

            OnPropertyChanged(nameof(SelectedProduct));

            MessageBox.Show(
                "Product added successfully.");
        }

        private async Task UpdateProductAsync()
        {
            if (SelectedProduct == null)
                return;

            if (!ValidateProduct())
                return;

            await _productRepository
                .UpdateProductAsync(SelectedProduct);

            await LoadProductsAsync();

            MessageBox.Show(
                "Product updated successfully.");
        }

        private async Task DeleteProductAsync()
        {
            if (SelectedProduct == null)
                return;

            await _productRepository
                .DeleteProductAsync(
                    SelectedProduct.Id);

            await LoadProductsAsync();

            SelectedProduct = new Product();

            OnPropertyChanged(nameof(SelectedProduct));

            MessageBox.Show(
                "Product deleted successfully.");
        }

        private bool ValidateProduct()
        {
            if (string.IsNullOrWhiteSpace(
                SelectedProduct.Name))
            {
                MessageBox.Show(
                    "Product name is required.");

                return false;
            }

            if (string.IsNullOrWhiteSpace(
                SelectedProduct.SKU))
            {
                MessageBox.Show(
                    "SKU is required.");

                return false;
            }

            if (SelectedProduct.Price <= 0)
            {
                MessageBox.Show(
                    "Price must be greater than zero.");

                return false;
            }

            if (SelectedProduct.StockQuantity < 0)
            {
                MessageBox.Show(
                    "Invalid stock quantity.");

                return false;
            }

            if (SelectedProduct.LowStockThreshold < 0)
            {
                MessageBox.Show(
                    "Invalid low stock threshold.");

                return false;
            }

            return true;
        }
    }
}