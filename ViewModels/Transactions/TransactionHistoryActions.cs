using StoreManagementSystem.ViewModels.Transactions;
using StoreManagementSystem.Views;
using System.Windows;

namespace StoreManagementSystem.ViewModels
{
    public partial class TransactionHistoryViewModel
    {
        private void ViewDetails(
            object? parameter)
        {
            if (SelectedTransaction == null)
            {
                MessageBox.Show(
                    "Please select a transaction.");

                return;
            }

            TransactionDetailsViewModel
                detailsViewModel =
                    new TransactionDetailsViewModel(
                        SelectedTransaction);

            TransactionDetailsWindow
                detailsWindow =
                    new TransactionDetailsWindow();

            detailsWindow.DataContext =
                detailsViewModel;

            detailsWindow.ShowDialog();
        }
    }
}