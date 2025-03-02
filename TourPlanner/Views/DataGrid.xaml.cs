using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TourPlanner.Views;

public partial class DataGrid : UserControl
{
    public ObservableCollection<RowData> TableData { get; set; }
    
    public DataGrid()
    {
        InitializeComponent();
        
        TableData =
        [
            new RowData { Date = "Row 1 - A", Duration = "Row 1 - B", Distance = "Row 1 - C" },
            new RowData { Date = "Row 2 - A", Duration = "Row 2 - B", Distance = "Row 2 - C" },
            new RowData { Date = "Row 3 - A", Duration = "Row 3 - B", Distance = "Row 3 - C" },
            new RowData { Date = "Row 4 - A", Duration = "Row 4 - B", Distance = "Row 4 - C" }
        ];

        DataContext = this;
    }
    
    public class RowData
    {
        public string Date { get; set; }
        public string Duration { get; set; }
        public string Distance { get; set; }
    }
}