using LiveCharts;
using LiveCharts.Wpf;
using StoreManagementSystem.Repositories;
using StoreManagementSystem.Services;
using System.Windows;

namespace StoreManagementSystem.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        // =========================
        // REPOSITORIES
        // =========================

        private readonly ProductRepository
            _productRepository;

        private readonly TransactionRepository
            _transactionRepository;

        // =========================
        // USER INFO
        // =========================

        public bool IsCashier =>
            CurrentUserService.CurrentUser?.Role
            == "Cashier";

        public string WelcomeMessage =>
            CurrentUserService.CurrentUser != null
                ? $"Welcome Back, {CurrentUserService.CurrentUser.FullName}"
                : "Welcome";

        public Visibility RevenueVisibility =>
            IsCashier
                ? Visibility.Collapsed
                : Visibility.Visible;

        public Visibility AnalyticsVisibility =>
            IsCashier
                ? Visibility.Collapsed
                : Visibility.Visible;

        // =========================
        // KPI
        // =========================

        private int _productsCount;

        public int ProductsCount
        {
            get => _productsCount;
            set
            {
                _productsCount = value;
                OnPropertyChanged();
            }
        }

        private int _lowStockCount;

        public int LowStockCount
        {
            get => _lowStockCount;
            set
            {
                _lowStockCount = value;
                OnPropertyChanged();
            }
        }

        private decimal _todaySales;

        public decimal TodaySales
        {
            get => _todaySales;
            set
            {
                _todaySales = value;
                OnPropertyChanged();
            }
        }

        private decimal _monthlyRevenue;

        public decimal MonthlyRevenue
        {
            get => _monthlyRevenue;
            set
            {
                _monthlyRevenue = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GrowthRate));
            }
        }

        public string GrowthRate
        {
            get
            {
                if (MonthlyRevenue >= 50000)
                    return "+25%";

                if (MonthlyRevenue >= 20000)
                    return "+15%";

                return "+5%";
            }
        }

        // =========================
        // CHARTS
        // =========================

        public SeriesCollection SalesSeries
        {
            get;
            set;
        } = new();

        public string[] SalesLabels
        {
            get;
            set;
        } = Array.Empty<string>();

        public SeriesCollection TopProductsSeries
        {
            get;
            set;
        } = new();

        public string[] TopProductsLabels
        {
            get;
            set;
        } = Array.Empty<string>();

        public SeriesCollection InventorySeries
        {
            get;
            set;
        } = new();

        // =========================
        // CONSTRUCTOR
        // =========================

        public DashboardViewModel()
        {
            _productRepository =
                new ProductRepository();

            _transactionRepository =
                new TransactionRepository();

            _ = InitializeDashboardAsync();
        }

        // =========================
        // INITIALIZATION
        // =========================

        private async Task InitializeDashboardAsync()
        {
            try
            {
                await LoadDashboardDataAsync();

                await LoadChartsAsync();

                await LoadWeeklySalesChartAsync();

                await LoadTopProductsChartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Dashboard loading failed:\n{ex.Message}");
            }
        }

        // =========================
        // LOAD DASHBOARD DATA
        // =========================

        private async Task LoadDashboardDataAsync()
        {
            ProductsCount =
                await _productRepository
                .GetProductsCountAsync();

            LowStockCount =
                await _productRepository
                .GetLowStockCountAsync();

            TodaySales =
                await _transactionRepository
                .GetTodaySalesAsync();

            MonthlyRevenue =
                await _transactionRepository
                .GetMonthlyRevenueAsync();
        }

        // =========================
        // TOP PRODUCTS CHART
        // =========================

        private async Task LoadTopProductsChartAsync()
        {
            var products =
                await _transactionRepository
                .GetTopSellingProductsAsync();

            TopProductsLabels =
                products.Keys.ToArray();

            TopProductsSeries =
                new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Sold",
                        Values =
                            new ChartValues<int>(
                                products.Values)
                    }
                };

            OnPropertyChanged(
                nameof(TopProductsSeries));

            OnPropertyChanged(
                nameof(TopProductsLabels));
        }

        // =========================
        // WEEKLY SALES CHART
        // =========================

        private async Task LoadWeeklySalesChartAsync()
        {
            var sales =
                await _transactionRepository
                .GetWeeklySalesAsync();

            SalesSeries =
                new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Sales",
                        Values =
                            new ChartValues<double>(
                                sales)
                    }
                };

            SalesLabels =
                new[]
                {
                    "6 Days",
                    "5 Days",
                    "4 Days",
                    "3 Days",
                    "2 Days",
                    "Yesterday",
                    "Today"
                };

            OnPropertyChanged(
                nameof(SalesSeries));

            OnPropertyChanged(
                nameof(SalesLabels));
        }

        // =========================
        // INVENTORY CHART
        // =========================

        private async Task LoadChartsAsync()
        {
            int inStock =
                await _productRepository
                .GetInStockCountAsync();

            int lowStock =
                await _productRepository
                .GetLowStockCountAsync();

            int outOfStock =
                await _productRepository
                .GetOutOfStockCountAsync();

            InventorySeries =
                new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "In Stock",
                        Values =
                            new ChartValues<int>
                            {
                                inStock
                            },
                        DataLabels = true
                    },

                    new PieSeries
                    {
                        Title = "Low Stock",
                        Values =
                            new ChartValues<int>
                            {
                                lowStock
                            },
                        DataLabels = true
                    },

                    new PieSeries
                    {
                        Title = "Out Of Stock",
                        Values =
                            new ChartValues<int>
                            {
                                outOfStock
                            },
                        DataLabels = true
                    }
                };

            OnPropertyChanged(
                nameof(InventorySeries));
        }
    }
}