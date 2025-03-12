using System.Collections.ObjectModel;
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
            set { _menuItems = value; OnPropertyChanged(); }
        }

        private int _countColumns = 5; 
        public int CountColumns
        {
            get { return _countColumns; }
            set { _countColumns = value; OnPropertyChanged(); }
        }

        public ICommand MenuItemCommand { get; }

        public NavbarViewModel()
        {
            MenuItems = ["File", "Edit", "Options", "Settings", "Help"];

            MenuItemCommand = new RelayCommand(menuItem => ExecuteMenuItemCommand(menuItem as string ?? string.Empty));
        }

        private void ExecuteMenuItemCommand(string menuItem)
        {
            Console.WriteLine($"Menu item clicked: {menuItem}");
        }
    }
}
