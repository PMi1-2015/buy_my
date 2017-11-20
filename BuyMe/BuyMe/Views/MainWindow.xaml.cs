using System.Collections.ObjectModel;
using System.Windows;
using BuyMe.ViewModels;
using BuyMe.DataAccess;
using BuyMe.Models;

namespace BuyMe.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //using(ShoppingListDbContext db = new ShoppingListDbContext())
            //{
            //    db.Categories.Add(new Category
            //    {
            //        Name = "Meal"
            //    });
            //    db.Categories.Add(new Category
            //    {
            //        Name = "Music"
            //    });
            //}
            //DataContext = new SelectListsViewModel();
        }
    }
}
