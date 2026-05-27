using StoreManagementSystem.Commands;
using StoreManagementSystem.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace StoreManagementSystem.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        // =========================
        // PRODUCTS
        // =========================

        public ObservableCollection<Product> Products { get; set; }

        public ObservableCollection<Product> AllProducts { get; set; }

        public ObservableCollection<Product> CartItems { get; set; }

        // =========================
        // SELECTED PRODUCT
        // =========================

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

        // =========================
        // SEARCH
        // =========================

        private string _searchText = "";

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

        // =========================
        // DASHBOARD STATS
        // =========================

        public int TotalProducts =>
            Products.Count;

        public int LowStockCount =>
            Products.Count(p => p.StockQuantity <= 5);

        public decimal InventoryValue =>
            Products.Sum(p => p.Price * p.StockQuantity);

        // =========================
        // POS TOTALS
        // =========================

        public decimal SubTotal =>
            CartItems.Sum(p => p.Price);

        public decimal VAT =>
            SubTotal * 0.14m;

        public decimal FinalTotal =>
            SubTotal + VAT;

        // =========================
        // COMMANDS
        // =========================

        public ICommand AddCommand { get; set; }

        public ICommand UpdateCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand AddToCartCommand { get; set; }

        public ICommand CheckoutCommand { get; set; }

        // =========================
        // CONSTRUCTOR
        // =========================

        public ProductViewModel()
        {
            Products = new ObservableCollection<Product>();

            AllProducts = new ObservableCollection<Product>();

            CartItems = new ObservableCollection<Product>();

            // Sample Products

            var keyboard = new Product
            {
                Name = "Keyboard",
                SKU = "KB001",
                Price = 500,
                StockQuantity = 10
            };

            var mouse = new Product
            {
                Name = "Mouse",
                SKU = "MS001",
                Price = 250,
                StockQuantity = 3
            };

            Products.Add(keyboard);
            Products.Add(mouse);

            AllProducts.Add(keyboard);
            AllProducts.Add(mouse);

            // Commands

            AddCommand = new RelayCommand(AddProduct);

            UpdateCommand = new RelayCommand(UpdateProduct);

            DeleteCommand = new RelayCommand(DeleteProduct);

            AddToCartCommand = new RelayCommand(AddToCart);

            CheckoutCommand = new RelayCommand(Checkout);
        }

        // =========================
        // ADD PRODUCT
        // =========================

        private void AddProduct(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(SelectedProduct.Name) ||
                string.IsNullOrWhiteSpace(SelectedProduct.SKU) ||
                SelectedProduct.Price <= 0 ||
                SelectedProduct.StockQuantity < 0)
            {
                MessageBox.Show("Please enter valid product data.");

                return;
            }

            var newProduct = new Product
            {
                Name = SelectedProduct.Name,
                SKU = SelectedProduct.SKU,
                Price = SelectedProduct.Price,
                StockQuantity = SelectedProduct.StockQuantity
            };

            AllProducts.Add(newProduct);

            FilterProducts();

            SelectedProduct = new Product();

            RefreshDashboard();
        }

        // =========================
        // UPDATE PRODUCT
        // =========================

        private void UpdateProduct(object? parameter)
        {
            RefreshDashboard();
        }

        // =========================
        // DELETE PRODUCT
        // =========================

        private void DeleteProduct(object? parameter)
        {
            if (SelectedProduct != null)
            {
                AllProducts.Remove(SelectedProduct);

                FilterProducts();

                SelectedProduct = new Product();

                RefreshDashboard();
            }
        }

        // =========================
        // SEARCH FILTER
        // =========================

        private void FilterProducts()
        {
            Products.Clear();

            var filteredProducts =
                string.IsNullOrWhiteSpace(SearchText)
                ? AllProducts
                : new ObservableCollection<Product>(
                    AllProducts.Where(p =>
                        p.Name.Contains(
                            SearchText,
                            System.StringComparison.OrdinalIgnoreCase)));

            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }

            RefreshDashboard();
        }

        // =========================
        // ADD TO CART
        // =========================

        private void AddToCart(object? parameter)
        {
            if (SelectedProduct != null)
            {
                CartItems.Add(SelectedProduct);

                RefreshCart();
            }
        }

        // =========================
        // CHECKOUT
        // =========================

        private void Checkout(object? parameter)
        {
            if (CartItems.Count == 0)
            {
                MessageBox.Show("Cart is empty.");

                return;
            }

            foreach (var item in CartItems)
            {
                item.StockQuantity--;
            }

            CartItems.Clear();

            RefreshCart();

            RefreshDashboard();

            MessageBox.Show("Checkout completed successfully.");
        }

        // =========================
        // REFRESH DASHBOARD
        // =========================

        private void RefreshDashboard()
        {
            OnPropertyChanged(nameof(TotalProducts));

            OnPropertyChanged(nameof(LowStockCount));

            OnPropertyChanged(nameof(InventoryValue));
        }

        // =========================
        // REFRESH CART
        // =========================

        private void RefreshCart()
        {
            OnPropertyChanged(nameof(SubTotal));

            OnPropertyChanged(nameof(VAT));

            OnPropertyChanged(nameof(FinalTotal));
        }
    }
}