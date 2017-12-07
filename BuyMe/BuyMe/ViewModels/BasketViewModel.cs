using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BuyMe.DataAccess;
using BuyMe.Models;

namespace BuyMe.ViewModels
{
    class BasketViewModel : INotifyPropertyChanged
    {
        private ShoppingListDbContext db;
        private Window currentWindow;
        private ObservableCollection<Product> selectedProducts;
       
        public ShoppingList SelectedShoppingList;
        public double SelectedProductsTotalPrice => selectedProducts.Sum(product => product.Price);

        public ObservableCollection<Product> SelectedProducts
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
            selectedProducts = SelectedShoppingList.Products as ObservableCollection<Product>;
        }));

        private CustomCommand clearSelectedCommand;
        public CustomCommand ClearSelectedCommand => clearSelectedCommand ?? (clearSelectedCommand = new CustomCommand(obj =>

        {
            if (obj is IEnumerable<Product> selected)
            {
                foreach (Product product in selected)
                {
                    SelectedShoppingList.Products.Remove(product);
                }
                db.SaveChanges();
            }
        }));

        private CustomCommand backCommand;
        public CustomCommand BackCommand => backCommand ?? (backCommand = new CustomCommand(obj =>
        {
            currentWindow.DialogResult = true;
        }));

        private CustomCommand buyMoreCommand;
        public CustomCommand BuyMoreCommand => buyMoreCommand ?? (buyMoreCommand = new CustomCommand(obj =>
        {

        }));

        public BasketViewModel(Window currentWindow, ShoppingList shoppingList)
        {
            db = new ShoppingListDbContext();
            SelectedShoppingList = shoppingList;
            this.currentWindow = currentWindow;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
