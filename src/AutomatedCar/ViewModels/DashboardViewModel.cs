namespace AutomatedCar.ViewModels
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using Avalonia;
    using ReactiveUI;

    public class DashboardViewModel : ViewModelBase
    {
        public static AutomatedCar controlledCar;
        public static VirtualFunctionBus vfb;

        public DashboardViewModel(Models.AutomatedCar car)
        {
            controlledCar = car;
            vfb = controlledCar.VirtualFunctionBus;
        }

        public Models.AutomatedCar ControlledCar
        {
            get => controlledCar;
            private set
            {
                this.RaiseAndSetIfChanged(ref controlledCar, value);
                vfb = controlledCar.VirtualFunctionBus;
            }
        }

        public SystemComponents.VirtualFunctionBus Vfb
        {
            get => vfb;
            private set
            {
                this.RaiseAndSetIfChanged(ref vfb, value);
            }
        }
    }
}