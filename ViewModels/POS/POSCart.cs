using StoreManagementSystem.Models;
using System.Linq;
using System.Windows;

namespace StoreManagementSystem.ViewModels
{
    public partial class POSViewModel
    {
        private void AddToCart(
            object? parameter)
        {
            if (SelectedProduct == null)
                return;

            if (SelectedProduct.StockQuantity
                <= 0)
            {
                MessageBox.Show(
                    "Product out of stock.");

                return;
            }

            var existingItem =
                CartItems.FirstOrDefault(c =>
                    c.Product.Id ==
                    SelectedProduct.Id);

            if (existingItem != null)
            {
                if (existingItem.Quantity
                    >= SelectedProduct.StockQuantity)
                {
                    MessageBox.Show(
                        "No more stock available.");

                    return;
                }

                existingItem.Quantity++;
            }
            else
            {
                CartItems.Add(new CartItem
                {
                    Product =
                        SelectedProduct,

                    Quantity = 1
                });
            }

            RefreshTotals();
        }

        private void RemoveFromCart(
            object? parameter)
        {
            if (parameter is not CartItem item)
                return;

            CartItems.Remove(item);

            RefreshTotals();
        }

        private void RefreshTotals()
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