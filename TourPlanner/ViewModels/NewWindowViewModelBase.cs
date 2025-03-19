using System.Windows;
using System.Windows.Input;
using TourPlanner.MVVM;

namespace TourPlanner.ViewModels;

public abstract class NewWindowViewModelBase: ViewModelBase
{
    public ICommand CloseCommand { get; }
    public ICommand SaveCommand { get; protected set; }

    
    public Window? NewWindow { get; set; }

    protected NewWindowViewModelBase()
    {
        CloseCommand = new RelayCommand(_ => CloseWindow());
        SaveCommand = new RelayCommand(_ => Save());
    }
    
    protected void CloseWindow()
    {
        NewWindow?.Close();
    }

    protected abstract void Save();
}