using StoreManagementSystem.Models;
using StoreManagementSystem.Services;
using System.Windows;

namespace StoreManagementSystem.ViewModels
{
    public partial class POSViewModel
    {
        private async Task CheckoutAsync()
        {
            if (CartItems.Count == 0)
            {
                MessageBox.Show(
                    "Cart is empty.");

                return;
            }

            if (CurrentUserService.CurrentUser == null)
            {
                MessageBox.Show(
                    "No logged in user found.");

                return;
            }

            bool confirm =
                MessageBox.Show(
                    "Are you sure you want to complete checkout?",
                    "Checkout Confirmation",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question)
                == MessageBoxResult.Yes;

            if (!confirm)
                return;

            try
            {
                Transaction transaction =
                    new Transaction
                    {
                        UserId =
                            CurrentUserService
                            .CurrentUser.UserId,

                        TransactionDate =
                            DateTime.Now
                    };

                foreach (var cartItem
                    in CartItems)
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

                RefreshTotals();

                await LoadProductsAsync();

                MessageBox.Show(
                    "Checkout completed successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Checkout failed:\n{ex.Message}");
            }
        }
    }
}