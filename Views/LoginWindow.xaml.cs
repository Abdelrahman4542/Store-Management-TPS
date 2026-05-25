using System.Windows;
using StoreManagementSystem.Services;

namespace StoreManagementSystem.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

        AuthService authService = new AuthService();

            bool isConnected = authService.TestConnection();

            MessageBox.Show(
                isConnected
                ? "Database Connected Successfully"
                : "Database Connection Failed"
            );
        }
    }

}
