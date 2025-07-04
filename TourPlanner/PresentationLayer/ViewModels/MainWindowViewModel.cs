using TourPlanner.DataAccessLayer;
using TourPlanner.DataAccessLayer.Repositories;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public NavbarViewModel NavbarVM { get; }
        public TourViewModel TourVm { get; }
        public InputWithButtonViewModel InputVM { get; }
        public TourLogsViewModel TourLogsVm { get; }

        public MainWindowViewModel()
        {
            TourLogsVm = new TourLogsViewModel();
            TourVm = new TourViewModel(TourLogsVm);
            NavbarVM = new NavbarViewModel();
            InputVM = new InputWithButtonViewModel();
        }

    }
}