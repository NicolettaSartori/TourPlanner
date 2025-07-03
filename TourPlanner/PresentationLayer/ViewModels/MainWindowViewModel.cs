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
            var dbContextFactory = new AppdDbContextFactory();
            var tourRepository = new TourRepository(dbContextFactory.CreateDbContext());
            var tourLogRepository = new TourLogRepository(dbContextFactory.CreateDbContext());

            TourLogsVm = new TourLogsViewModel(tourLogRepository);
            TourVm = new TourViewModel(TourLogsVm, tourRepository);
            NavbarVM = new NavbarViewModel();
            InputVM = new InputWithButtonViewModel();
        }

    }
}