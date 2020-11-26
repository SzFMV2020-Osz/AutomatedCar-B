namespace AutomatedCar.ViewModels
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using AutomatedCar.SystemComponents.Packets;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Interactivity;
    using ReactiveUI;

    public class DashboardViewModel : ViewModelBase
    {
        public static AutomatedCar controlledCar;

        public DashboardViewModel(Models.AutomatedCar car)
        {
            controlledCar = car;
        }

        public Models.AutomatedCar ControlledCar
        {
            get => controlledCar;
            private set
            {
                this.RaiseAndSetIfChanged(ref controlledCar, value);
            }
        }
        
        public void OnClickCommand()
        {
            this.ControlledCar.HealthPoints = 100;
        }   
    }
}