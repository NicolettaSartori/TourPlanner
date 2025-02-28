using System.Windows;
using System.Windows.Controls;

namespace TourPlanner.Views;

public partial class Navbar : UserControl
{
    public Navbar()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty MenuItemsProperty =
        DependencyProperty.Register(nameof(MenuItems), typeof(string[]), typeof(Navbar),
            new PropertyMetadata(null, OnMenuItemsChanged));

    public string[] MenuItems
    {
        get => (string[])GetValue(MenuItemsProperty);
        set => SetValue(MenuItemsProperty, value);
    }
    public string[] ProcessedMenuItems { get; private set; } = [];
    public int CountColumns { get; private set; }

    private static void OnMenuItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (Navbar)d;
        string[] processedItems = (e.NewValue as string[])?
            // replaces "" with an empty string, because it's not possible to do a real empty string <sys:String>
            .Select(s => s.Equals("\"\"") ? "" : s)
            .ToArray() ?? [];
        control.ProcessedMenuItems = processedItems;
        control.CountColumns = processedItems.Length;
    }
}