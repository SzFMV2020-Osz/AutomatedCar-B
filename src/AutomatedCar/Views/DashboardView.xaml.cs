using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AutomatedCar.Views {
    public class DashboardView : UserControl {
        public DashboardView () {
            InitializeComponent ();
        }

        private void InitializeComponent () {
            AvaloniaXamlLoader.Load (this);
        }
    }
}