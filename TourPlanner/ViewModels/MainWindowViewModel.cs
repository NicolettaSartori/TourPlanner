namespace TourPlanner.ViewModels
{
    public class MainWindowViewModel: ViewModelBase
    {
        public NavbarViewModel NavbarVM { get; }
        public ListViewModel ListVM { get; }
        public InputWithButtonViewModel InputVM { get; }
        public DataGridViewModel DataGridVM { get; }

        public MainWindowViewModel()
        {
            NavbarVM = new NavbarViewModel();
            ListVM = new ListViewModel();
            InputVM = new InputWithButtonViewModel();
            DataGridVM = new DataGridViewModel();
        }
    }
}