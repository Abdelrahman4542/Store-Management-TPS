namespace StoreManagementSystem.ViewModels
{
    public partial class TransactionHistoryViewModel
    {
        private async void LoadTransactionsAsync()
        {
            Transactions.Clear();

            AllTransactions.Clear();

            var transactions =
                await _transactionRepository
                .GetAllTransactionsAsync();

            foreach (var transaction
                in transactions)
            {
                var items =
                    await _transactionRepository
                    .GetTransactionItemsAsync(
                        transaction.TransactionId);

                foreach (var item in items)
                {
                    transaction.Items.Add(item);
                }

                Transactions.Add(transaction);

                AllTransactions.Add(transaction);
            }

            RefreshDashboard();
        }
    }
}