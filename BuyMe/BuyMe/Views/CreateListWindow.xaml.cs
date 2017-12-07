using System.Windows;
using System.Windows.Forms.VisualStyles;
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
    }
}
