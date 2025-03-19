namespace TourPlanner.ViewModels
{
    public class MainWindowViewModel: ViewModelBase
    {
        public NavbarViewModel NavbarVM { get; }
        public TourViewModel TourVm { get; }
        public InputWithButtonViewModel InputVM { get; }
        public TourLogsViewModel TourLogsVm { get; }

        public MainWindowViewModel()
        {
            NavbarVM = new NavbarViewModel();
            InputVM = new InputWithButtonViewModel();
            TourLogsVm = new TourLogsViewModel();
            TourVm = new TourViewModel(TourLogsVm);
        }
    }
}