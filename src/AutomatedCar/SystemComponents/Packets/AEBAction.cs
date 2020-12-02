namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;
    public class EABAction : ReactiveObject, IPowerTrainAction, IReadOnlyAEBAction
    {
        public bool Active { get; set; }
        private double breakpedal;
        public double Breakpedal { get => this.breakpedal; set => this.RaiseAndSetIfChanged(ref this.breakpedal, value); }

        private string message = "Teszt";
        public string Message { get => this.message; set => this.RaiseAndSetIfChanged(ref this.message, value); }
    }
}