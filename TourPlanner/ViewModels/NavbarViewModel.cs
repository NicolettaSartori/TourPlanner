﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.MVVM;

namespace TourPlanner.ViewModels
{
    public class NavbarViewModel : ViewModelBase
    {
        private ObservableCollection<string> _menuItems;
        public ObservableCollection<string> MenuItems
        {
            get { return _menuItems; }
            set
            {
                _menuItems = value;
                CountColumns = _menuItems.Count;
                OnPropertyChanged();
            }
        }

        private int _countColumns; 
        public int CountColumns
        {
            get { return _countColumns; }
            set { _countColumns = value; OnPropertyChanged(); }
        }

        public ICommand MenuItemCommand { get; }

        public NavbarViewModel()
        {
            MenuItems = ["File", "Edit", "Options", string.Empty, string.Empty, "Help"];
            CountColumns = MenuItems.Count;
            MenuItemCommand = new RelayCommand(menuItem => ExecuteMenuItemCommand(menuItem as string ?? string.Empty));
        }

        private void ExecuteMenuItemCommand(string menuItem)
        {
            Console.WriteLine($"Menu item clicked: {menuItem}");
        }
    }
}
