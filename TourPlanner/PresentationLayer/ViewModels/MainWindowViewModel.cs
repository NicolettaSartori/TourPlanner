using TourPlanner.DataAccessLayer;
using TourPlanner.BusinessLayer.Services;
using TourPlanner.Infrastructure;

namespace TourPlanner.PresentationLayer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public NavbarViewModel NavbarVM { get; }
        public TourViewModel TourVm { get; }
        public InputWithButtonViewModel InputVM { get; }
        public TourLogsViewModel TourLogsVm { get; }
        private readonly OpenRouteServiceClient _routeService = new(ConfigurationHelper.Configuration);


        public MainWindowViewModel()
        {
            TourLogsVm = new TourLogsViewModel();
            TourVm = new TourViewModel(TourLogsVm, _routeService);
            NavbarVM = new NavbarViewModel();
            InputVM = new InputWithButtonViewModel();
        }

    }
}