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
                // يخفي إدارة المخزون
                InventoryButton.Visibility = Visibility.Collapsed;

                // Products تتحول إلى POS
                ProductsButton.Content = "Point Of Sale";

                // يخفي Revenue و Growth
                RevenueSection.Visibility = Visibility.Collapsed;

                // يخفي الشارتات
                SalesChartSection.Visibility = Visibility.Collapsed;

                // يخفي Analytics
                AnalyticsSection.Visibility = Visibility.Collapsed;
            }

            // ================= ADMIN =================
            // Admin يشوف كل حاجة
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
