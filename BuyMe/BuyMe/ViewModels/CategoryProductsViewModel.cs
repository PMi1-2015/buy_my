using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using BuyMe.DataAccess;
using BuyMe.Models;
using BuyMe.Views;
using MessageBox = System.Windows.MessageBox;

namespace BuyMe.ViewModels
{
    class CategoryProductsViewModel : INotifyPropertyChanged
    {
        private readonly Window currentWindow;
        private readonly ShoppingListDbContext db;

        public ObservableCollection<Category> Categories { get; set; }

        private Category selectedCategory;
        private Product selectedProduct;

        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
            }
        }

        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }

        }

        private CustomCommand backCommand;
        public CustomCommand BackCommand => backCommand ?? (backCommand = new CustomCommand(obj =>
        {
            currentWindow.DialogResult = true;
        }));

        private CustomCommand addNewProduct;
        public CustomCommand AddNewCommand => addNewProduct ?? (addNewProduct = new CustomCommand(obj =>
        {
            var addNewProductWindow = new AddProductWindow(SelectedCategory);
            addNewProductWindow.ShowDialog();
        }));

        private CustomCommand editSelectedCommand;
        public CustomCommand EditSelectedCommand => editSelectedCommand ?? (editSelectedCommand = new CustomCommand(
                                                        obj =>
                                                        {
                                                            var product = obj as Product;
                                                            var editProductWindow =
                                                                new AddProductWindow(SelectedCategory, product);
                                                            editProductWindow.ShowDialog();
                                                        }));

        private CustomCommand deleteSelectedCommand;

        public CustomCommand DeleteSelectedCommand =>
            deleteSelectedCommand ?? (deleteSelectedCommand = new CustomCommand(
                obj =>
                {
                    var product = obj as Product;
                    DialogResult deleteConfirmationResult = System.Windows.Forms.MessageBox.Show(
                        $"Are you sure to delete {product.Name} product",
                        "Delete confirmation", MessageBoxButtons.YesNo);

                    if (deleteConfirmationResult != DialogResult.Yes) return;

                    //Possible cascade deleting
                    db.Products.Remove(product);
                    db.SaveChanges();
                }));
        public CategoryProductsViewModel(Window window)
        {
            db = new ShoppingListDbContext();
            currentWindow = window;
            db.Categories.Load();
            Categories = db.Categories.Local;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<T> ToObservableCollection<T>(IQueryable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}
