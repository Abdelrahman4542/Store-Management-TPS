using StoreManagementSystem.Models;

namespace StoreManagementSystem.ViewModels.Transactions
{
    public class TransactionDetailsViewModel
        : BaseViewModel
    {
        private Transaction
            _currentTransaction;

        public Transaction
            CurrentTransaction
        {
            get => _currentTransaction;

            set
            {
                _currentTransaction = value;

                OnPropertyChanged();
            }
        }

        public TransactionDetailsViewModel(
            Transaction transaction)
        {
            _currentTransaction =
                transaction;
        }
    }
}