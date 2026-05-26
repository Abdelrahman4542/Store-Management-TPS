using System.Windows;
using StoreManagementSystem.Models;
using StoreManagementSystem.Services;

namespace StoreManagementSystem.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }


    private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            AuthService authService = new AuthService();

            User? loggedInUser = authService.Login(username, password);

            if (loggedInUser != null)
            {
                CurrentUserService.CurrentUser = loggedInUser;

                MessageBox.Show(
                    $"Welcome {loggedInUser.FullName}\nRole: {loggedInUser.Role}"
                );

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }
    }


}
