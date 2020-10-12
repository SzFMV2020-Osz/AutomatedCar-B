namespace AutomatedCar.ViewModels
{
    using AutomatedCar.Models;
    using AutomatedCar.SystemComponents;
    using ReactiveUI;

    public class DashboardViewModel : ViewModelBase
    {
        private AutomatedCar controlledCar;
        private HumanMachineInterface hmi;

        public DashboardViewModel(Models.AutomatedCar controlledCar)
        {
            this.ControlledCar = controlledCar;
            this.hmi = this.controlledCar.HumanMachineInterface;
        }

        public Models.AutomatedCar ControlledCar
        {
            get => this.controlledCar;
            private set
            {
                this.RaiseAndSetIfChanged(ref this.controlledCar, value);
                this.hmi = this.controlledCar.HumanMachineInterface;
            }
        }

        public SystemComponents.HumanMachineInterface HMI
        {
            get => this.hmi;
            private set => this.RaiseAndSetIfChanged(ref this.hmi, value);
        }
    }
}