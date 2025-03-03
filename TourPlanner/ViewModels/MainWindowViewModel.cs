namespace TourPlanner.ViewModels
{
    public class MainWindowViewModel
    {
        public NavbarViewModel NavbarVM { get; set; }
        public ListViewModel ListVM { get; set; }
        public InputWithButtonViewModel InputVM { get; set; }
        public DataGridViewModel DataGridVM { get; set; }

        public MainWindowViewModel()
        {
            NavbarVM = new NavbarViewModel();
            ListVM = new ListViewModel();
            InputVM = new InputWithButtonViewModel();
            DataGridVM = new DataGridViewModel();
        }
    }
}