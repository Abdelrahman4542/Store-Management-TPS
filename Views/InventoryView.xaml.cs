using System.Windows.Controls;
using StoreManagementSystem.ViewModels;

namespace StoreManagementSystem.Views
{
    public partial class InventoryView : UserControl
    {
        public InventoryView()
        {
            InitializeComponent();


        DataContext = new ProductViewModel();
        }
    }


}
