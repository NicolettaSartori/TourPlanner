using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.MVVM;
using TourPlanner.Views;
using TourPlanner.Windows;

namespace TourPlanner.ViewModels;

public class TourLogsViewModel : NewWindowViewModelBase
{
    private ObservableCollection<TourLog> _tourLogs = [];
    public ObservableCollection<TourLog> TourLogs
    {
        get => _tourLogs;
        set
        {
            _tourLogs = value;
            OnPropertyChanged();
        }
    }
    
    private TourLog? _selectedLog;
    public TourLog? SelectedLog
    {
        get => _selectedLog;
        set
        {
            SetField(ref _selectedLog, value);
            OnPropertyChanged();
            CommandManager.InvalidateRequerySuggested();
        }
    }
    
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }
    public TourViewModel? TourViewModel { get; set; }

    public TourLogsViewModel()
    {
        AddCommand = new RelayCommand(_ => AddItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem(), _ => SelectedLog != null);
    }
    
    private void AddItem()
    {
        NewWindow = new NewTourWindow();
        NewWindow.DataContext = this;
        NewWindow.ShowDialog();
    }
        
    private void DeleteItem()
    {
        Console.WriteLine("Delete Item");
        if (SelectedLog != null)
        {
            TourViewModel?.DeleteTourLogFromSelected(SelectedLog);
            TourLogs.Remove(SelectedLog);
        }
    }
    
    protected override void Save()
    { 
        NewWindow?.Close();
    }
}