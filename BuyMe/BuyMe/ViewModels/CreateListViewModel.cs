using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BuyMe.Views;

namespace BuyMe.ViewModels
{
    class CreateListViewModel : INotifyPropertyChanged
    {
        private Window currentWindow;
        private string name;
        private string imagePath;
        private DateTime reminderTime;

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

        public DateTime ReminderTime
        {
            get => reminderTime;
            set
            {
                reminderTime = value;
                OnPropertyChanged("ReminderTime");
            }
        }

        private CustomCommand backCommand;

        public CustomCommand BackCommand => backCommand ?? (backCommand = new CustomCommand(obj =>
        {
            currentWindow.DialogResult = true;
        }));

        public CreateListViewModel(Window window)
        {
            currentWindow = window;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
