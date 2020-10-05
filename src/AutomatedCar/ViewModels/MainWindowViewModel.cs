namespace AutomatedCar.ViewModels
{
    using AutomatedCar.Models;
    using AutomatedCar.Visualization;
    using ReactiveUI;

    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase dashboard;
        private ViewModelBase courseDisplay;
        private World world;

        public World World
        {
            get => this.world;
            private set => this.RaiseAndSetIfChanged(ref this.world, value);
        }

        public MainWindowViewModel(World world)
        {
            VisualizationWorld visualizationWorld = new VisualizationWorld(world);
            this.CourseDisplay = new CourseDisplayViewModel(visualizationWorld);
            this.Dashboard = new DashboardViewModel(world.ControlledCar);
            this.World = visualizationWorld;
        }

        public ViewModelBase CourseDisplay
        {
            get => this.courseDisplay;
            private set => this.RaiseAndSetIfChanged(ref this.courseDisplay, value);
        }

        public ViewModelBase Dashboard
        {
            get => this.dashboard;
            private set => this.RaiseAndSetIfChanged(ref this.dashboard, value);
        }
    }
}