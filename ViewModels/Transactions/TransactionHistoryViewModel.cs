using StoreManagementSystem.Commands;
using StoreManagementSystem.Models;
using StoreManagementSystem.Repositories;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace StoreManagementSystem.ViewModels
{
    public partial class TransactionHistoryViewModel
        : BaseViewModel
    {
        private readonly TransactionRepository
            _transactionRepository;

        public ObservableCollection<Transaction>
            Transactions
        {
            get;
            set;
        }

        public ObservableCollection<Transaction>
            AllTransactions
        {
            get;
            set;
        }

        private Transaction?
            _selectedTransaction;

        public Transaction?
            SelectedTransaction
        {
            get => _selectedTransaction;

            set
            {
                _selectedTransaction = value;

                OnPropertyChanged();
            }
        }

        private string _searchText =
            string.Empty;

        public string SearchText
        {
            get => _searchText;

            set
            {
                _searchText = value;

                OnPropertyChanged();

                FilterTransactions();
            }
        }

        public int TotalTransactions =>
            Transactions.Count;

        public decimal TotalRevenue =>
            Transactions.Sum(t => t.FinalTotal);

        public ICommand ViewDetailsCommand
        {
            get;
            set;
        }

        public TransactionHistoryViewModel()
        {
            _transactionRepository =
                new TransactionRepository();

            Transactions =
                new ObservableCollection<Transaction>();

            AllTransactions =
                new ObservableCollection<Transaction>();

            LoadTransactionsAsync();

            ViewDetailsCommand =
                new RelayCommand(ViewDetails);
        }
    }
}