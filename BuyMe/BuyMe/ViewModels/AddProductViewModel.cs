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
using Microsoft.Win32;

namespace BuyMe.ViewModels
{
    class AddProductViewModel: INotifyPropertyChanged
    {
        private ShoppingListDbContext db;
        private readonly Window currentWindow;
        private readonly Category currentCategory;

        private string name;
        private string imagePath;
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

        public string ImagePath
        {
            get => imagePath;
            set
            {
                imagePath = value;
                OnPropertyChanged("ImagePath");
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
            db.Products.Local.Add(new Product
            {
                Name = this.Name,
                Category = currentCategory,
                Price = this.Price,
                ImagePath = this.ImagePath,
                Description = this.Description
            });
            db.SaveChanges();
            currentWindow.DialogResult = true;
        }));

        private CustomCommand addImageCommand;
        public CustomCommand AddImageCommand => addImageCommand ?? (addImageCommand = new CustomCommand(obj =>
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter =
                    "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };

            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult == true)
            {
                ImagePath = dialog.FileName;
            }
        }));

        public AddProductViewModel(Window currentWindow, Category currentCategory)
        {
            db = new ShoppingListDbContext();
            this.currentWindow = currentWindow;
            this.currentCategory = currentCategory;
        }

        //public AddProductViewModel(Window currentWindow, Category currentCategory, Product productToEdit)
        //{
        //    db = new ShoppingListDbContext();
        //    this.currentWindow = currentWindow;
        //    this.currentCategory = currentCategory;
        //    Name = productToEdit.Name;
        //    ImagePath = productToEdit.ImagePath;
        //    Price = productToEdit.Price;
        //    Description = productToEdit.Description;
        //}
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
