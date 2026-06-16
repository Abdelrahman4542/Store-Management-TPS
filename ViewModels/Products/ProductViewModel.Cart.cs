using StoreManagementSystem.Models;
using StoreManagementSystem.Services;
using System.Linq;
using System.Windows;

namespace StoreManagementSystem.ViewModels
{
    public partial class ProductViewModel
    {
        private void AddToCart(object? parameter)
        {
            if (SelectedProduct == null)
                return;

            if (SelectedProduct.StockQuantity <= 0)
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
                existingItem.Quantity++;
            }
            else
            {
                CartItems.Add(new CartItem
                {
                    Product = SelectedProduct,
                    Quantity = 1
                });
            }

            RefreshCart();
        }

        private async Task CheckoutAsync()
        {
            if (CartItems.Count == 0)
            {
                MessageBox.Show(
                    "Cart is empty.");

                return;
            }

            Transaction transaction =
                new Transaction
                {
                    UserId =
                        CurrentUserService
                        .CurrentUser!.UserId,

                    TransactionDate =
                        DateTime.Now
                };

            foreach (var cartItem in CartItems)
            {
                transaction.Items.Add(
                    new TransactionItem
                    {
                        ProductId =
                            cartItem.Product.Id,

                        ProductName =
                            cartItem.Product.Name,

                        Quantity =
                            cartItem.Quantity,

                        UnitPrice =
                            cartItem.Product.Price
                    });
            }

            await _transactionRepository
                .SaveTransactionAsync(
                    transaction);

            CartItems.Clear();

            RefreshCart();

            LoadProductsAsync();

            MessageBox.Show(
                "Checkout completed successfully.");
        }
    }
}