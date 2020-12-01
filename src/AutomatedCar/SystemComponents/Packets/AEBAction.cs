namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;
    public class EABAction : ReactiveObject, IPowerTrainAction
    {
        public bool Active {get; set;}

        private double breakpedal;
        public double Breakpedal { get => this.breakpedal; set => this.RaiseAndSetIfChanged(ref this.breakpedal, value); }

    }
}