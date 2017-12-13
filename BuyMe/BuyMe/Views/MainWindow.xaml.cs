using System;
using System.Collections.ObjectModel;
using System.Linq;
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

            DataContext = new SelectListsViewModel(this);
            this.Title = "Shopping List";
            //Memory.Db = new ShoppingListDbContext();
        }
    }
}
