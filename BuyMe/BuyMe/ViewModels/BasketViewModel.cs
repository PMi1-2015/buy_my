using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using BuyMe.DataAccess;
using BuyMe.Models;
using BuyMe.Views;

namespace BuyMe.ViewModels
{
    class BasketViewModel : INotifyPropertyChanged
    {
        private readonly ShoppingListDbContext db;
        private readonly Window currentWindow;
        private ICollection<Product> selectedProducts;
       
        public ShoppingList SelectedShoppingList { get; set; }
        public double SelectedProductsTotalPrice => selectedProducts?.Sum(product => product.Price) ?? 0;

        public ICollection<Product> SelectedProducts
        {
            get => selectedProducts;
            set
            {
                selectedProducts = value; 
                OnPropertyChanged("SelectedProducts");
            }
        }

        private CustomCommand selectAllCommand;
        public CustomCommand SelectAllCommand => selectAllCommand ?? (selectAllCommand = new CustomCommand(obj =>
        {
            selectedProducts = SelectedShoppingList.Products;
        }));

        private CustomCommand clearSelectedCommand;
        public CustomCommand ClearSelectedCommand => clearSelectedCommand ?? (clearSelectedCommand = new CustomCommand(obj =>

        {
            var selected = (IList)obj;
            ObservableCollection<Product> selectedCollection = new ObservableCollection<Product>(selected.Cast<Product>());

            foreach (Product product in selectedCollection)
            {
                SelectedShoppingList.Products.Remove(product);
            }
            db.SaveChanges();
        }));

        private CustomCommand backCommand;
        public CustomCommand BackCommand => backCommand ?? (backCommand = new CustomCommand(obj =>
        {
            currentWindow.DialogResult = true;
        }));

        private CustomCommand buyMoreCommand;
        public CustomCommand BuyMoreCommand => buyMoreCommand ?? (buyMoreCommand = new CustomCommand(obj =>
        {
            var categoryProductWindow = new CategoryProductsWindow();
            categoryProductWindow.ShowDialog();
        }));

        public BasketViewModel(Window currentWindow, int shoppingListId)
        {
            db = new ShoppingListDbContext();
            SelectedShoppingList = db.ShoppingLists.First(list => list.Id == shoppingListId);
            this.currentWindow = currentWindow;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
