using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BuyMe.DataAccess;
using BuyMe.Models;

namespace BuyMe.ViewModels
{
    class SelectListsViewModel : INotifyPropertyChanged
    {
        private ShoppingList _selectedList;
        public ObservableCollection<ShoppingList> ShoppingLists { get; set; }
        public ShoppingList SelectedList
        {
            get => _selectedList;
            set
            {
                _selectedList = value;
                OnPropertyChanged("SelectedList");
            }
        }

        public SelectListsViewModel()
        {
            var db = new ShoppingListDbContext();
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
