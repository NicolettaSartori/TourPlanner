using System.Windows.Input;
using TourPlanner.PresentationLayer.MVVM;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public class InputWithButtonViewModel : ViewModelBase
    {
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }

        public InputWithButtonViewModel()
        {
            SearchText = "Search";
            SearchCommand = new RelayCommand(_ => ExecuteSearch());
        }

        private void ExecuteSearch()
        {
        }
    }
}