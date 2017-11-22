using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BuyMe.DataAccess;
using BuyMe.Models;
using BuyMe.Views;
using MessageBox = System.Windows.Forms.MessageBox;

namespace BuyMe.ViewModels
{
    class SelectListsViewModel : Window, INotifyPropertyChanged
    {
        private ShoppingListDbContext db;
        private Window _currentWindow;
        private ShoppingList _selectedList;
        public ObservableCollection<ShoppingList> ShoppingLists { get; set; }

        private CustomCommand _createNewListCommand;
        public CustomCommand CreateNewListCommand => _createNewListCommand ?? (_createNewListCommand = new CustomCommand(obj =>
        {
            var createListWindow = new CreateListWindow {Owner = _currentWindow};
            createListWindow.ShowDialog();
        }));

        private CustomCommand _deleteListCommand;
        public CustomCommand DeleteListCommand => _deleteListCommand ?? (_deleteListCommand = new CustomCommand(obj =>
        {
            if (!(obj is ShoppingList toDeleteShoppingList)) return;

            DialogResult deleteConfirmationResult = MessageBox.Show(
                $"Are you sure to delete {toDeleteShoppingList.ListName} list",
                "Delete confirmation", MessageBoxButtons.YesNo);

            if (deleteConfirmationResult != System.Windows.Forms.DialogResult.Yes) return;

            ShoppingLists.Remove(toDeleteShoppingList);
            db.SaveChanges();
        }, obj => ShoppingLists.Count > 0));

        private CustomCommand _editListCommand;

        public CustomCommand EditListCommand => _editListCommand ?? (_editListCommand = new CustomCommand(obj =>
        {
            if (!(obj is ShoppingList toEditList)) return;

            var editListWindow = new CategoryProductsWindow() {Owner = _currentWindow};
            editListWindow.ShowDialog();
        }));

        public ShoppingList SelectedList
        {
            get => _selectedList;
            set
            {
                _selectedList = value;
                OnPropertyChanged("SelectedList");
            }
        }

        public SelectListsViewModel(Window currentWindow)
        {
            db = new ShoppingListDbContext();
            _currentWindow = currentWindow;

            db.ShoppingLists.Load();
            
            ShoppingLists = db.ShoppingLists.Local;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
