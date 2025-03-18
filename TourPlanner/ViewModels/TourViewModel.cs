using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.BusinessLayer.Factories;
using TourPlanner.Models;
using TourPlanner.MVVM;
using TourPlanner.Windows;

namespace TourPlanner.ViewModels
{
    public class TourViewModel : NewWindowViewModelBase
    {
        public static ObservableCollection<Tour>? Tours { get; private set; }
        
        private Tour? _selectedTour;

        private const string ImagePath = "/Images/";
        private readonly TourLogsViewModel _tourLogsViewModel;

        public Tour? SelectedTour
        {
            get => _selectedTour;
            set
            {
                SetField(ref _selectedTour, value);
                ImageUri = $"{ImagePath}{_selectedTour?.Id.ToString()}.png";
                _tourLogsViewModel.TourLogs = new ObservableCollection<TourLog>(_selectedTour.Logs);
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested();
            }
        }
        
        private string _imageUri;
        public string ImageUri {
            get => _imageUri;
            private set
            {
                _imageUri = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public TourViewModel(TourLogsViewModel tourLogsViewModel)
        {
            _tourLogsViewModel = tourLogsViewModel;
            _tourLogsViewModel.TourViewModel = this;
            Tours = TourFactory.GetTours();
            SelectedTour = Tours.FirstOrDefault();
            AddCommand = new RelayCommand(_ => AddItem());
            DeleteCommand = new RelayCommand(_ => DeleteItem(), _ => SelectedTour != null);
        }
        
        private void AddItem()
        {
            NewWindow = new NewTourWindow();
            NewWindow.DataContext = this;
            NewWindow.ShowDialog();
        }
        
        private void DeleteItem()
        {
            if (SelectedTour != null)
            {
                int index = Tours.IndexOf(SelectedTour);
                Tours.Remove(SelectedTour);
                // select the tour before 
                SelectedTour = (index <= 0) ? Tours.FirstOrDefault() : Tours[index - 1];
            }
        }

        public void DeleteTourLogFromSelected(TourLog log)
        {
            _selectedTour?.Logs.Remove(log);
            OnPropertyChanged(nameof(SelectedTour));
        }

        protected override void Save()
        { 
            NewWindow?.Close();
        }
    }
}