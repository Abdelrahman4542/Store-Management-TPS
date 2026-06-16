using StoreManagementSystem.Commands;
using StoreManagementSystem.Models;
using StoreManagementSystem.Repositories;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace StoreManagementSystem.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        private readonly ProductRepository _productRepository;

        private readonly TransactionRepository _transactionRepository;

        public ObservableCollection<Product> Products { get; set; }

        public ObservableCollection<Product> AllProducts { get; set; }

        public ObservableCollection<CartItem> CartItems { get; set; }

        private Product _selectedProduct = new();

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        private string _searchText = string.Empty;

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

        public int TotalProducts =>
            Products.Count;

        public int LowStockCount =>
            Products.Count(p => p.IsLowStock);

        public decimal InventoryValue =>
            Products.Sum(p => p.TotalValue);

        public decimal SubTotal =>
            CartItems.Sum(c => c.LineTotal);

        public decimal VAT =>
            SubTotal * 0.14m;

        public decimal FinalTotal =>
            SubTotal + VAT;

        public ICommand AddCommand { get; set; }

        public ICommand UpdateCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand AddToCartCommand { get; set; }

        public ICommand CheckoutCommand { get; set; }

        public ProductViewModel()
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

            AddCommand =
                new AsyncRelayCommand(
                    async _ => await AddProductAsync());

            UpdateCommand =
                new AsyncRelayCommand(
                    async _ => await UpdateProductAsync());

            DeleteCommand =
                new AsyncRelayCommand(
                    async _ => await DeleteProductAsync());

            AddToCartCommand =
                new RelayCommand(AddToCart);

            CheckoutCommand =
                new AsyncRelayCommand(
                    async _ => await CheckoutAsync());
        }
    }
}