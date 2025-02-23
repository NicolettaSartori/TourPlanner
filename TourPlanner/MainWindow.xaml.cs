using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TourPlanner;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<RowData> TableData { get; set; }
    
    public MainWindow()
    {
        InitializeComponent();
        
        TableData = new ObservableCollection<RowData>
        {
            new RowData { Date = "Row 1 - A", Duration = "Row 1 - B", Distance = "Row 1 - C" },
            new RowData { Date = "Row 2 - A", Duration = "Row 2 - B", Distance = "Row 2 - C" },
            new RowData { Date = "Row 3 - A", Duration = "Row 3 - B", Distance = "Row 3 - C" },
            new RowData { Date = "Row 4 - A", Duration = "Row 4 - B", Distance = "Row 4 - C" }
        };

        DataContext = this;
    }
}

public class RowData
{
    public string Date { get; set; }
    public string Duration { get; set; }
    public string Distance { get; set; }
}
