using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StoreManagementSystem.Models
{
    public class Product : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _name = string.Empty;

        private string _sku = string.Empty;

        private decimal _price;

        private int _stockQuantity;
        private int _lowStockThreshold = 5;
        public int Id { get; set; }

        public string Name
        {
            get => _name;

            set
            {
                _name = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public string SKU
        {
            get => _sku;

            set
            {
                _sku = value;

                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get => _price;

            set
            {
                _price = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public int StockQuantity
        {
            get => _stockQuantity;

            set
            {
                _stockQuantity = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLowStock));
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public int LowStockThreshold
        {
            get => _lowStockThreshold;

            set
            {
                _lowStockThreshold = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLowStock));
            }
        }
        public bool IsLowStock =>
            StockQuantity <= LowStockThreshold;
        public decimal TotalValue =>
            Price * StockQuantity;

        // =========================
        // IDataErrorInfo
        // =========================

        public string Error => string.Empty;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Name):

                        if (string.IsNullOrWhiteSpace(Name))
                            return "Product name is required.";

                        if (Name.Length < 2)
                            return "Product name is too short.";

                        break;

                    case nameof(SKU):

                        if (string.IsNullOrWhiteSpace(SKU))
                            return "SKU is required.";

                        break;

                    case nameof(Price):

                        if (Price <= 0)
                            return "Price must be greater than zero.";

                        break;

                    case nameof(StockQuantity):

                        if (StockQuantity < 0)
                            return "Stock quantity cannot be negative.";

                        break;
                    case nameof(LowStockThreshold):

                        if (LowStockThreshold < 1)
                            return "Threshold must be greater than zero.";

                        break;
                }

                return string.Empty;
            }
        }

        // =========================
        // Property Changed
        // =========================

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}