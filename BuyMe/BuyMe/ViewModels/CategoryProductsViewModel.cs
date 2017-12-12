using System;
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
        //private readonly ShoppingListMemory.DbContext Memory.Db;

        public ObservableCollection<Category> Categories { get; set; }

        private Category selectedCategory;
        private Product selectedProduct;
        private double amount;
        private int shoppingListId;

        public double Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

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
                    Memory.Db.Products.Remove(product);
                    Memory.Db.SaveChanges();
                }));

        private CustomCommand incCommand;
        public CustomCommand IncCommand => incCommand ?? (incCommand = new CustomCommand(obj =>
        {
            if ((int)Amount >= 0) Amount++;
        }));

        private CustomCommand decCommand;
        public CustomCommand DecCommand => decCommand ?? (decCommand = new CustomCommand(obj =>
        {
            if ((int)Amount > 0) Amount--;
        }));

        private CustomCommand submitCommand;
        public CustomCommand SubmitCommand => submitCommand ?? (submitCommand = new CustomCommand(obj =>
        {
            if ((int) Amount <= 0) return;
                //SelectedProduct = Memory.Db.Products.First(p => p.Name ==);
                Order toChange;
            try
            {
                 toChange = Memory.Db.Orders?.First(x =>
                    x.ShoppingList.Id == shoppingListId && x.Product.Name == SelectedProduct.Name);
            }
            catch (Exception e)
            {
                toChange = null;
            }
           
            if (toChange != null)
            {
                toChange.Amount = Amount;
                toChange.Product = SelectedProduct;
                toChange.ShoppingList = Memory.Db.ShoppingLists.First(list => list.Id == shoppingListId);
            }
            else
            {
                Order order = new Order
                {
                    Amount = Amount,
                    Product = SelectedProduct,
                    ShoppingList = Memory.Db.ShoppingLists.First(list => list.Id == shoppingListId)
                };
                Memory.Db.Orders.Add(order);
            }
            Memory.Db.SaveChanges();
            MessageBox.Show("Order added!");
            //Memory.Db.Orders?.Remove(order);
            //Memory.Db.Orders.Add(order);
            //Amount++;
        }));

        public CategoryProductsViewModel(Window window, int shoppingListId)
        {
            //Memory.Db = new ShoppingListMemory.DbContext();
            currentWindow = window;
            Memory.Db.Categories.Load();
            Categories = Memory.Db.Categories.Local;
            //Amount = 0;
            //?.Where(order => order.ShoppingList.Id == shoppingListId)
       //     try
        //    {
       //         Amount = Memory.Db.Orders.Local?.First(o => o.Product.Name == SelectedProduct.Name && o.ShoppingList.Id == shoppingListId).Amount ?? 0;
      //      }
       //     catch (Exception e)
       //     {
                Amount = 0;
       //     }
           
            this.shoppingListId = shoppingListId;
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
