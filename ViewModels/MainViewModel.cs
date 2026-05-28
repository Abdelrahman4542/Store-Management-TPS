using StoreManagementSystem.Commands;
using StoreManagementSystem.Services;
using StoreManagementSystem.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace StoreManagementSystem.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        // =========================
        // CURRENT VIEW
        // =========================

        private UserControl? _currentView;

        public UserControl? CurrentView
        {
            get => _currentView;

            set
            {
                _currentView = value;

                OnPropertyChanged();
            }
        }

        // =========================
        // DASHBOARD VISIBILITY
        // =========================

        private Visibility _dashboardVisibility =
            Visibility.Visible;

        public Visibility DashboardVisibility
        {
            get => _dashboardVisibility;

            set
            {
                _dashboardVisibility = value;

                OnPropertyChanged();
            }
        }

        // =========================
        // CONTENT VISIBILITY
        // =========================

        private Visibility _contentVisibility =
            Visibility.Collapsed;

        public Visibility ContentVisibility
        {
            get => _contentVisibility;

            set
            {
                _contentVisibility = value;

                OnPropertyChanged();
            }
        }

        // =========================
        // ROLE-BASED VISIBILITY
        // =========================

        public Visibility InventoryVisibility =>
            IsCashier
            ? Visibility.Collapsed
            : Visibility.Visible;

        public Visibility ProductsVisibility =>
            IsCashier
            ? Visibility.Collapsed
            : Visibility.Visible;

        public Visibility RevenueVisibility =>
            IsCashier
            ? Visibility.Collapsed
            : Visibility.Visible;

        public Visibility AnalyticsVisibility =>
            IsCashier
            ? Visibility.Collapsed
            : Visibility.Visible;

        // =========================
        // USER INFO
        // =========================

        public bool IsCashier =>
            CurrentUserService.CurrentUser?.Role
            == "Cashier";

        public string WelcomeMessage =>
            CurrentUserService.CurrentUser != null
            ? $"Welcome, {CurrentUserService.CurrentUser.FullName}"
            : "Welcome";

        // =========================
        // DASHBOARD DATA
        // =========================

        public int ProductsCount => 150;

        public int LowStockCount => 12;

        public decimal TodaySales => 3420;

        public decimal MonthlyRevenue => 105315;

        public string GrowthRate => "+12.5%";

        // =========================
        // COMMANDS
        // =========================

        public ICommand OpenDashboardCommand
        {
            get;
        }

        public ICommand OpenInventoryCommand
        {
            get;
        }

        public ICommand OpenProductsCommand
        {
            get;
        }

        public ICommand OpenPOSCommand
        {
            get;
        }

        public ICommand OpenTransactionsCommand
        {
            get;
        }

        public ICommand LogoutCommand
        {
            get;
        }

        // =========================
        // CONSTRUCTOR
        // =========================

        public MainViewModel()
        {
            OpenDashboardCommand =
                new RelayCommand(OpenDashboard);

            OpenInventoryCommand =
                new RelayCommand(OpenInventory);

            OpenProductsCommand =
                new RelayCommand(OpenProducts);

            OpenPOSCommand =
                new RelayCommand(OpenPOS);

            OpenTransactionsCommand =
                new RelayCommand(OpenTransactions);

            LogoutCommand =
                new RelayCommand(Logout);
        }

        // =========================
        // DASHBOARD
        // =========================

        private void OpenDashboard(
            object? parameter)
        {
            CurrentView = null;

            DashboardVisibility =
                Visibility.Visible;

            ContentVisibility =
                Visibility.Collapsed;
        }

        // =========================
        // INVENTORY
        // =========================

        private void OpenInventory(
            object? parameter)
        {
            CurrentView =
                new InventoryView();

            DashboardVisibility =
                Visibility.Collapsed;

            ContentVisibility =
                Visibility.Visible;
        }

        // =========================
        // PRODUCTS
        // =========================

        private void OpenProducts(
            object? parameter)
        {
            CurrentView =
                new ProductsView();

            DashboardVisibility =
                Visibility.Collapsed;

            ContentVisibility =
                Visibility.Visible;
        }

        // =========================
        // POS
        // =========================

        private void OpenPOS(
            object? parameter)
        {
            CurrentView =
                new POSView();

            DashboardVisibility =
                Visibility.Collapsed;

            ContentVisibility =
                Visibility.Visible;
        }

        // =========================
        // TRANSACTIONS
        // =========================

        private void OpenTransactions(
            object? parameter)
        {
            TransactionHistoryWindow
                transactionWindow =
                    new TransactionHistoryWindow();

            transactionWindow.ShowDialog();
        }

        // =========================
        // LOGOUT
        // =========================

        private void Logout(
            object? parameter)
        {
            CurrentUserService.CurrentUser =
                null;

            LoginWindow loginWindow =
                new LoginWindow();

            loginWindow.Show();

            foreach (Window window
                in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    window.Close();

                    break;
                }
            }
        }
    }
}