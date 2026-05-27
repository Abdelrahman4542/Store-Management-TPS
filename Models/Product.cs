using System.ComponentModel;

namespace StoreManagementSystem.Models
{
    public class Product : INotifyPropertyChanged
    {
        private string _name = string.Empty;
        private string _sku = string.Empty;
        private decimal _price;
        private int _stockQuantity;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public string SKU
        {
            get => _sku;
            set
            {
                _sku = value;
                OnPropertyChanged(nameof(SKU));
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            set
            {
                _stockQuantity = value;

                OnPropertyChanged(nameof(StockQuantity));
                OnPropertyChanged(nameof(IsLowStock));
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        // Low Stock Alert
        public bool IsLowStock => StockQuantity < 5;

        // Total Inventory Value
        public decimal TotalValue => Price * StockQuantity;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}