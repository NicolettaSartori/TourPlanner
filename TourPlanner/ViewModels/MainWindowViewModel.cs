namespace TourPlanner.ViewModels
{
    public class MainWindowViewModel: ViewModelBase
    {
        public NavbarViewModel NavbarVM { get; }
        public TourViewModel TourVm { get; }
        public InputWithButtonViewModel InputVM { get; }

        public MainWindowViewModel()
        {
            NavbarVM = new NavbarViewModel();
            TourVm = new TourViewModel();
            InputVM = new InputWithButtonViewModel();
        }
    }
}