﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BuyMe.DataAccess;
using BuyMe.Models;
using BuyMe.Views;
using Microsoft.Win32;

namespace BuyMe.ViewModels
{
    class CreateListViewModel : INotifyPropertyChanged
    {
        private readonly Window currentWindow;

        private string name;
        private string imagePath;
        private DateTime reminderTime;
        private string description;

        public ShoppingList ShoppingListToEdit;

        public string ListName
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("ListName");
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

        public DateTime ReminderTime
        {
            get => reminderTime;
            set
            {
                reminderTime = value;
                OnPropertyChanged("ReminderTime");
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

        private bool isCheckOn;
        public bool IsCheckBoxOn
        {
            get => isCheckOn;
            set
            {
                isCheckOn = value;
                OnPropertyChanged("IsCheckBoxOn");
            }
        }

        private string datePickerVisibility;

        public string DatePickerVisibility
        {
            get => datePickerVisibility;
            set
            {
                datePickerVisibility = value;
                OnPropertyChanged("DatePickerVisibility");
            }
        }

        private CustomCommand backCommand;
        public CustomCommand BackCommand => backCommand ?? (backCommand = new CustomCommand(obj =>
        {
            currentWindow.DialogResult = true;
        }));

        private CustomCommand toggleDatePickerCommand;
        public CustomCommand ToggleDatePickerCommand =>
            toggleDatePickerCommand ?? (toggleDatePickerCommand = new CustomCommand(
                obj =>
                {
                    DatePickerVisibility = IsCheckBoxOn ? "Visible" : "Hidden";
                }));

        private CustomCommand submitCommand;
        public CustomCommand SubmitCommand => submitCommand ?? (submitCommand = new CustomCommand(obj =>
        {
            if (ShoppingListToEdit != null)
            {
                ShoppingListToEdit.ListName = ListName;
                ShoppingListToEdit.Description = Description;
                ShoppingListToEdit.ImagePath = ImagePath;
                ShoppingListToEdit.ReminderTime = ReminderTime;
                ShoppingListToEdit.LastEditTime = DateTime.Now;
            }
            else
            {
                GetUniqName();
                SetDefaultPictureWhenItsNotSetted();

                Memory.Db.ShoppingLists.Add(new ShoppingList
                {
                    ListName = ListName,
                    ImagePath = ImagePath,
                    ReminderTime = ReminderTime,
                    LastEditTime = DateTime.Now,
                    Description = Description
                });
            }            
            Memory.Db.SaveChanges();
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

        private void GetUniqName()
        {
            Memory.Db.ShoppingLists.Load();
            int index = 0;
            while (true)
            {

                if (Memory.Db.ShoppingLists.Local.All(list => list.ListName != ListName))
                {
                    break;
                }
                if (index != 0)
                {
                    ListName = ListName.Substring(0, ListName.Length - 1);
                }              
                ListName += index++;
            }
        }

        private void SetDefaultPictureWhenItsNotSetted()
        {
            if (ImagePath == "../Images/plus.png")
            {
                ImagePath = "../Images/cart.png";
            }
        }

        public CreateListViewModel(Window window)
        {
            currentWindow = window;
            IsCheckBoxOn = false;
            DatePickerVisibility = "Hidden";
            ListName = "DefaultName";
            ImagePath = "../Images/plus.png";
            ReminderTime = DateTime.Now;
        }

        public CreateListViewModel(Window window, ShoppingList shoppingList)
        {
            currentWindow = window;
            IsCheckBoxOn = shoppingList.ReminderTime == new DateTime() ? false : true;
            DatePickerVisibility = IsCheckBoxOn ? "Visible" : "Hidden";
            ListName = shoppingList.ListName;
            ImagePath = shoppingList.ImagePath;
            ReminderTime = shoppingList.ReminderTime;
            Description = shoppingList.Description;
            ShoppingListToEdit = Memory.Db.ShoppingLists.First(list => list.Id == shoppingList.Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
