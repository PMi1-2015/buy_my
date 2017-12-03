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

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            DatePicker.Visibility = DatePicker.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
