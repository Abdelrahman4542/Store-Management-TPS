using StoreManagementSystem.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace StoreManagementSystem.ViewModels
{
    public partial class TransactionHistoryViewModel
    {
        private void FilterTransactions()
        {
            Transactions.Clear();

            var filteredTransactions =
                string.IsNullOrWhiteSpace(
                    SearchText)
                ? AllTransactions
                : new ObservableCollection<Transaction>(
                    AllTransactions.Where(t =>
                        t.TransactionId
                        .ToString()
                        .Contains(SearchText)
                        ||
                        t.Status.Contains(
                            SearchText,
                            System.StringComparison
                            .OrdinalIgnoreCase)));

            foreach (var transaction
                in filteredTransactions)
            {
                Transactions.Add(transaction);
            }

            RefreshDashboard();
        }

        private void RefreshDashboard()
        {
            OnPropertyChanged(
                nameof(TotalTransactions));

            OnPropertyChanged(
                nameof(TotalRevenue));
        }
    }
}