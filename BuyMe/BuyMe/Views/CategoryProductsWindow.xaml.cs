﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BuyMe.ViewModels;

namespace BuyMe.Views
{
    public partial class CategoryProductsWindow : Window
    {
        public CategoryProductsWindow(int shoppingListId)
        {
            InitializeComponent();
            DataContext = new CategoryProductsViewModel(this, shoppingListId);
            this.Title = "Category products";
        }
    }
}
