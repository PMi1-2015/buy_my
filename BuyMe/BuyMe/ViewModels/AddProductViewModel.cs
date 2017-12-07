using System;
using System.Collections.Generic;
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
    class AddProductViewModel: INotifyPropertyChanged
    {
        private ShoppingListDbContext db;
        private Window currentWindow;
        private Category currentCategory;
        private string name;
        private double price;
        private string description;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private CustomCommand backCommand;
        public CustomCommand BackCommand => backCommand ?? (backCommand = new CustomCommand(obj =>
        {
            currentWindow.DialogResult = true;
        }));

        private CustomCommand submitCommand;
        public CustomCommand SubmitCommand => submitCommand ?? (submitCommand = new CustomCommand(obj =>
        {
            db.Products.Add(new Product
            {
                Name = this.Name,
                Category = currentCategory,
                Price = this.Price
            });
            db.SaveChanges();
            currentWindow.DialogResult = true;
        }));

        public AddProductViewModel(Window currentWindow, Category currentCategory)
        {
            this.currentWindow = currentWindow;
            this.currentCategory = currentCategory;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
