using System.Windows;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using StoreManagementSystem.Services;

namespace StoreManagementSystem.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        LoadCurrentUser();
            ApplyRolePermissions();
        }

        private void LoadCurrentUser()
        {
            if (CurrentUserService.CurrentUser != null)
            {
                WelcomeText.Text =
                    $"Hello, {CurrentUserService.CurrentUser.FullName}...";
            }
        }

        private void ApplyRolePermissions()
        {
            if (CurrentUserService.CurrentUser == null)
                return;

            string? role = CurrentUserService.CurrentUser.Role;

            // ================= CASHIER =================

            if (role == "Cashier")
            {
                InventoryButton.Visibility = Visibility.Collapsed;

                ProductsButton.Content = "Point Of Sale";

                RevenueSection.Visibility = Visibility.Collapsed;

                SalesChartSection.Visibility = Visibility.Collapsed;

                AnalyticsSection.Visibility = Visibility.Collapsed;
            }

            // ================= ADMIN =================
            // Admin يشوف كل حاجة
        }

        private void InventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Visibility = Visibility.Visible;

            DashboardContent.Visibility = Visibility.Collapsed;

            MainContent.Content = new InventoryView();
        }

        private void ProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Visibility = Visibility.Visible;

            DashboardContent.Visibility = Visibility.Collapsed;

            MainContent.Content = new ProductsView();
        }

        private void POSBtn_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Visibility = Visibility.Visible;

            DashboardContent.Visibility = Visibility.Collapsed;

            MainContent.Content = new POSView();
        }
        private void DashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Visibility = Visibility.Collapsed;

          DashboardContent.Visibility = Visibility.Visible;


}

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentUserService.CurrentUser = null;

            LoginWindow loginWindow = new LoginWindow();

            loginWindow.Show();

            this.Close();
        }
    }

}
