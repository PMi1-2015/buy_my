using System.Windows;
using System.Windows.Forms.VisualStyles;
using BuyMe.Models;
using BuyMe.ViewModels;

namespace BuyMe.Views
{
    public partial class CreateListWindow : Window
    {
        public CreateListWindow()
        {
            InitializeComponent();
            DataContext = new CreateListViewModel(this);
        }

        public CreateListWindow(ShoppingList shoppingList)
        {
            InitializeComponent();
            DataContext = new CreateListViewModel(this, shoppingList);
        }
    }
}
