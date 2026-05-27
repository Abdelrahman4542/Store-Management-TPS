using System.Windows;
using StoreManagementSystem.ViewModels;
using StoreManagementSystem.Views;

namespace StoreManagementSystem
{
    public partial class MainWindow : Window
    {
        private readonly ProductViewModel _productViewModel = new();

        public MainWindow()
        {
            InitializeComponent();

            var inventoryView = new InventoryView
            {
                DataContext = _productViewModel
            };

            MainContent.Content = inventoryView;
        }

        private void InventoryBtn_Click(object sender, RoutedEventArgs e)
        {
            var inventoryView = new InventoryView
            {
                DataContext = _productViewModel
            };

            MainContent.Content = inventoryView;
        }

        private void ProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            var productsView = new ProductsView
            {
                DataContext = _productViewModel
            };

            MainContent.Content = productsView;
        }
        private void POSBtn_Click(object sender, RoutedEventArgs e)
        {
            var posView = new POSView
            {
                DataContext = _productViewModel
            };

            MainContent.Content = posView;
        }
    }
}