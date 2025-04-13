using System.Windows;
using System.Windows.Input;
using TourPlanner.PresentationLayer.MVVM;

namespace TourPlanner.PresentationLayer.ViewModels;

public abstract class NewWindowViewModelBase: ViewModelBase
{
    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CloseCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand EditCommand { get; }
    public ICommand UpdateCommand { get; }

    
    public Window? NewWindow { get; set; }

    protected NewWindowViewModelBase()
    {
        AddCommand = new RelayCommand(_ => AddItem());
        CloseCommand = new RelayCommand(_ => CloseWindow());
        SaveCommand = new RelayCommand(_ => Save(), _ => CanSave());
        DeleteCommand = new RelayCommand(_ => DeleteItem(), _ => CanDelete());
        EditCommand = new RelayCommand(_ => EditItem(), _ => CanEdit());
        UpdateCommand = new RelayCommand(_ => UpdateItem(), _ => CanUpdate());
    }
    
    protected virtual void CloseWindow()
    {
        NewWindow?.Close();
    }

    protected abstract void AddItem();
    protected abstract void Save();
    protected abstract void DeleteItem();
    protected abstract void EditItem();
    protected abstract void UpdateItem();
    protected abstract bool CanSave();
    protected abstract bool CanDelete();
    protected abstract bool CanEdit();
    protected abstract bool CanUpdate();
}