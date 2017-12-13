using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Runtime.CompilerServices;
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
        private Window currentWindow;
        private ShoppingList selectedList;
        public ObservableCollection<ShoppingList> ShoppingLists { get; set; }

        private CustomCommand createNewListCommand;
        public CustomCommand CreateNewListCommand => createNewListCommand ?? (createNewListCommand = new CustomCommand(obj =>
        {
            var createListWindow = new CreateListWindow {Owner = currentWindow};
            createListWindow.ShowDialog();
            Memory.Db.ShoppingLists.Load();
            ShoppingLists = Memory.Db.ShoppingLists.Local;
        }));

        private CustomCommand deleteListCommand;
        public CustomCommand DeleteListCommand => deleteListCommand ?? (deleteListCommand = new CustomCommand(obj =>
        {
            if (!(obj is ShoppingList toDeleteShoppingList)) return;

            DialogResult deleteConfirmationResult = MessageBox.Show(
                $"Are you sure to delete {toDeleteShoppingList.ListName} list",
                "Delete confirmation", MessageBoxButtons.YesNo);

            if (deleteConfirmationResult != System.Windows.Forms.DialogResult.Yes) return;

            ShoppingLists.Remove(toDeleteShoppingList);
            Memory.Db.SaveChanges();
        }, obj => ShoppingLists.Count > 0));

        private CustomCommand manageProductsCommand;
        public CustomCommand ManageProductsCommand => manageProductsCommand ?? (manageProductsCommand = new CustomCommand(obj =>
        {
            if (!(obj is ShoppingList toEditList)) return;

            var editListWindow = new BasketWindow(SelectedList.Id) {Owner = currentWindow};
            editListWindow.ShowDialog();
        }));

        private CustomCommand editListInfoCommand;
        public CustomCommand EditListInfoCommand => editListInfoCommand ?? (editListInfoCommand = new CustomCommand(obj =>
        {
            var createListWindow = new CreateListWindow(SelectedList) { Owner = currentWindow };
            

            createListWindow.ShowDialog();
            Memory.Db.ShoppingLists.Load();
            ShoppingLists = Memory.Db.ShoppingLists.Local;
        }));

        public ShoppingList SelectedList
        {
            get => selectedList;
            set
            {
                selectedList = value;
                OnPropertyChanged("SelectedList");
            }
        }

        public SelectListsViewModel(Window currentWindow)
        {
            this.currentWindow = currentWindow;

            Memory.Db.ShoppingLists.Load();
            
            ShoppingLists = Memory.Db.ShoppingLists.Local;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
