using System;
using System.ComponentModel;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;
using TourPlanner.PresentationLayer.ViewModels;
using System.Windows;


namespace TourPlanner.PresentationLayer.Views
{
    public partial class TourInfo : UserControl
    {
        public TourInfo()
        {
            InitializeComponent();
            Loaded += async (_, _) => await RouteWebView.EnsureCoreWebView2Async();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is TourViewModel oldVm)
            {
                oldVm.PropertyChanged -= OnVmPropertyChanged;
            }

            if (e.NewValue is TourViewModel newVm)
            {
                newVm.PropertyChanged += OnVmPropertyChanged;
                UpdateWebView(newVm.ImageUri); 
            }
        }

        private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TourViewModel.ImageUri) && sender is TourViewModel vm)
            {
                UpdateWebView(vm.ImageUri);
            }
        }

        private void UpdateWebView(string uri)
        {
            if (RouteWebView.CoreWebView2 != null && !string.IsNullOrWhiteSpace(uri))
            {
                RouteWebView.Source = new Uri(uri);
            }
        }
    }
}