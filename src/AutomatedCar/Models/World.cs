namespace AutomatedCar.Models
{
    using System.Collections.ObjectModel;
    using ReactiveUI;

    public class World : ReactiveObject
    {
        private AutomatedCar controlledCar;

        public static World Instance { get; } = new World();

        public ObservableCollection<WorldObject> WorldObjects { get; } = new ObservableCollection<WorldObject>();

        public AutomatedCar ControlledCar
        {
            get => this.controlledCar;
            set => this.RaiseAndSetIfChanged(ref this.controlledCar, value);
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public void AddObject(WorldObject worldObject)
        {
            this.WorldObjects.Add(worldObject);
        }
    }
}