namespace AutomatedCar.SystemComponents.Packets
{
    using ReactiveUI;
    public class AEBAction : ReactiveObject, IPowerTrainAction, IReadOnlyAEBAction
    {
        public bool Active { get; set; }

        private string message = "";
        public string Message { get => this.message; set => this.RaiseAndSetIfChanged(ref this.message, value); }
    }
}