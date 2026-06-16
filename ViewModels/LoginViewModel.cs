using StoreManagementSystem.Commands;
using StoreManagementSystem.Services;
using StoreManagementSystem.Views;
using System.Windows;
using System.Windows.Input;

namespace StoreManagementSystem.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        // =========================
        // SERVICES
        // =========================

        private readonly AuthService _authService;

        // =========================
        // USERNAME
        // =========================

        private string _username =
            string.Empty;

        public string Username
        {
            get => _username;

            set
            {
                _username = value;

                OnPropertyChanged();
            }
        }

        // =========================
        // PASSWORD
        // =========================

        private string _password =
            string.Empty;

        public string Password
        {
            get => _password;

            set
            {
                _password = value;

                OnPropertyChanged();
            }
        }

        // =========================
        // COMMANDS
        // =========================

        public ICommand LoginCommand
        {
            get;
        }

        // =========================
        // CONSTRUCTOR
        // =========================

        public LoginViewModel()
        {
            _authService =
                new AuthService();

            LoginCommand =
                new AsyncRelayCommand(
                    async _ => await LoginAsync());
        }

        // =========================
        // LOGIN
        // =========================

        private async Task LoginAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(
                    Username))
                {
                    MessageBox.Show(
                        "Please enter username.",
                        "Validation",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                if (string.IsNullOrWhiteSpace(
                    Password))
                {
                    MessageBox.Show(
                        "Please enter password.",
                        "Validation",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                var user =
                    await _authService.LoginAsync(
                        Username.Trim(),
                        Password);

                if (user == null)
                {
                    MessageBox.Show(
                        "Invalid username or password.",
                        "Login Failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return;
                }

                CurrentUserService.CurrentUser =
                    user;

                MainWindow mainWindow =
                    new MainWindow();

                mainWindow.Show();

                Window? loginWindow =
                    Application.Current.Windows
                    .OfType<LoginWindow>()
                    .FirstOrDefault();

                loginWindow?.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Login failed:\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}