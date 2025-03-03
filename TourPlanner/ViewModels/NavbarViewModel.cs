using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TourPlanner.ViewModels
{
    public class NavbarViewModel : INotifyPropertyChanged
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
            MenuItems = new ObservableCollection<string>
            {
                "File", "Edit", "Options", "Settings", "Help"
            };

            MenuItemCommand = new RelayCommand<string>(ExecuteMenuItemCommand);
        }

        private void ExecuteMenuItemCommand(string menuItem)
        {
            System.Diagnostics.Debug.WriteLine($"Menu item clicked: {menuItem}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
