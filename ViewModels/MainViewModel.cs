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
        // ROLE BASED ACCESS
        // =========================

        public bool IsCashier =>
            CurrentUserService.CurrentUser?.Role
            == "Cashier";

        public Visibility InventoryVisibility =>
            IsCashier
                ? Visibility.Collapsed
                : Visibility.Visible;

        public Visibility ProductsVisibility =>
            IsCashier
                ? Visibility.Collapsed
                : Visibility.Visible;

        // =========================
        // COMMANDS
        // =========================

        public ICommand OpenDashboardCommand { get; }

        public ICommand OpenInventoryCommand { get; }

        public ICommand OpenProductsCommand { get; }

        public ICommand OpenPOSCommand { get; }

        public ICommand OpenTransactionsCommand { get; }

        public ICommand LogoutCommand { get; }

        // =========================
        // CONSTRUCTOR
        // =========================

        public MainViewModel()
        {
            OpenDashboardCommand =
                new RelayCommand(_ => OpenDashboard());

            OpenInventoryCommand =
                new RelayCommand(_ => OpenInventory());

            OpenProductsCommand =
                new RelayCommand(_ => OpenProducts());

            OpenPOSCommand =
                new RelayCommand(_ => OpenPOS());

            OpenTransactionsCommand =
                new RelayCommand(_ => OpenTransactions());

            LogoutCommand =
                new RelayCommand(_ => Logout());

            CurrentView =
                new DashboardView();
        }

        // =========================
        // NAVIGATION METHODS
        // =========================

        private void OpenDashboard()
        {
            CurrentView =
                new DashboardView();
        }

        private void OpenInventory()
        {
            CurrentView =
                new InventoryView();
        }

        private void OpenProducts()
        {
            CurrentView =
                new ProductsView();
        }

        private void OpenPOS()
        {
            CurrentView =
                new POSView();
        }

        private void OpenTransactions()
        {
            try
            {
                TransactionHistoryWindow
                    transactionWindow =
                        new TransactionHistoryWindow();

                transactionWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to open transaction history.\n\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // =========================
        // LOGOUT
        // =========================

        private void Logout()
        {
            bool result =
                MessageBox.Show(
                    "Are you sure you want to logout?",
                    "Logout",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question)
                == MessageBoxResult.Yes;

            if (!result)
                return;

            CurrentUserService.CurrentUser =
                null;

            LoginWindow loginWindow =
                new LoginWindow();

            loginWindow.Show();

            Window? mainWindow =
                Application.Current.Windows
                .OfType<MainWindow>()
                .FirstOrDefault();

            mainWindow?.Close();
        }
    }
}