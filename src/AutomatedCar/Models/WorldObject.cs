namespace AutomatedCar.Models
{
    using ReactiveUI;

    public abstract class WorldObject : ReactiveObject
    {
        private int _x;
        private int _y;
        private int _angle = 91; // mocking

        public string CenterPoint = "100,0";

        public WorldObject(int x, int y, string filename)
        {
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.ZIndex = 1;
        }

        public int ZIndex { get; set; }
        public int Angle
        {
            get => this._angle;
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public int X
        {
            get => this._x;
            set => this.RaiseAndSetIfChanged(ref this._x, value);
        }

        public int Y
        {
            get => this._y;
            set => this.RaiseAndSetIfChanged(ref this._y, value);
        }

        public string Filename { get; set; }
    }
}