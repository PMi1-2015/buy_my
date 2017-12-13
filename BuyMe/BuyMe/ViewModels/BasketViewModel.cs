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
        private readonly Window currentWindow;
        private ObservableCollection<Order> selectedProducts;
        private ShoppingList selectedShoppingList;

        public ShoppingList SelectedShoppingList {
            get => selectedShoppingList;
            set
            {
                selectedShoppingList = value;
                OnPropertyChanged("SelectedShoppingList"); 
            } 
        }

        private double selectedProductsTotalPrice;
        public double SelectedProductsTotalPrice
        {
            get => selectedProductsTotalPrice;
            set
            {
                selectedProductsTotalPrice = value;
                OnPropertyChanged("SelectedProductsTotalPrice");
            }
        }

        public ObservableCollection<Order> SelectedProducts
        {
            get => selectedProducts;
            set
            {
                selectedProducts = value; 
                OnPropertyChanged("SelectedProducts");
            }
        }

        private CustomCommand selectionChangedCommand;
        public CustomCommand SelectionChangedCommand => selectionChangedCommand ?? (selectionChangedCommand = new CustomCommand(obj =>
        {
            var selected = (IList)obj;
            ObservableCollection<Order> selectedCollection = new ObservableCollection<Order>(selected.Cast<Order>());
            double toRet = 0;
            foreach (Order product in selectedCollection)
            {
                toRet+=(product.Product.Price * product.Amount);
            }
            SelectedProductsTotalPrice = toRet;
        }));

        private CustomCommand clearSelectedCommand;
        public CustomCommand ClearSelectedCommand => clearSelectedCommand ?? (clearSelectedCommand = new CustomCommand(obj =>

        {
            var selected = (IList)obj;
            ObservableCollection<Order> selectedCollection = new ObservableCollection<Order>(selected.Cast<Order>());

            foreach (Order product in selectedCollection)
            {
                SelectedShoppingList.Orders.Remove(product);
            }
            Memory.Db.SaveChanges();
            SelectedProductsTotalPrice = SelectedProducts?.Sum(o => o.Product.Price) ?? 0;
            SelectedShoppingList = SelectedShoppingList;
        }));

        private CustomCommand backCommand;
        public CustomCommand BackCommand => backCommand ?? (backCommand = new CustomCommand(obj =>
        {
            currentWindow.DialogResult = true;
        }));

        private CustomCommand buyMoreCommand;
        public CustomCommand BuyMoreCommand => buyMoreCommand ?? (buyMoreCommand = new CustomCommand(obj =>
        {
            var categoryProductWindow = new CategoryProductsWindow(SelectedShoppingList.Id);
            SelectedShoppingList = SelectedShoppingList;
            categoryProductWindow.ShowDialog();
        }));

        public BasketViewModel(Window currentWindow, int shoppingListId)
        {
            SelectedShoppingList = Memory.Db.ShoppingLists.First(list => list.Id == shoppingListId);
            this.currentWindow = currentWindow;
            SelectedProductsTotalPrice = selectedProducts?.Sum(o => o.Product.Price) ?? 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
