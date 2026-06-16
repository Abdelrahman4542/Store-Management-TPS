using StoreManagementSystem.Commands;
using StoreManagementSystem.Models;
using StoreManagementSystem.Repositories;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace StoreManagementSystem.ViewModels
{
    public partial class POSViewModel : BaseViewModel
    {
        private readonly ProductRepository
            _productRepository;

        private readonly TransactionRepository
            _transactionRepository;

        public ObservableCollection<Product>
            Products
        {
            get;
            set;
        }

        public ObservableCollection<Product>
            AllProducts
        {
            get;
            set;
        }

        public ObservableCollection<CartItem>
            CartItems
        {
            get;
            set;
        }

        private Product _selectedProduct =
            new();

        public Product SelectedProduct
        {
            get => _selectedProduct;

            set
            {
                _selectedProduct = value;

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

                FilterProducts();
            }
        }

        public decimal SubTotal =>
            CartItems.Sum(c => c.LineTotal);

        public decimal VAT =>
            SubTotal * 0.14m;

        public decimal FinalTotal =>
            SubTotal + VAT;

        public ICommand AddToCartCommand
        {
            get;
            set;
        }

        public ICommand RemoveFromCartCommand
        {
            get;
            set;
        }

        public ICommand CheckoutCommand
        {
            get;
            set;
        }

        public POSViewModel()
        {
            _productRepository =
                new ProductRepository();

            _transactionRepository =
                new TransactionRepository();

            Products =
                new ObservableCollection<Product>();

            AllProducts =
                new ObservableCollection<Product>();

            CartItems =
                new ObservableCollection<CartItem>();

            _ = LoadProductsAsync();

            AddToCartCommand =
                new RelayCommand(AddToCart);

            RemoveFromCartCommand =
                new RelayCommand(RemoveFromCart);

            CheckoutCommand =
                new AsyncRelayCommand(
                    async _ => await CheckoutAsync());
        }
    }
}