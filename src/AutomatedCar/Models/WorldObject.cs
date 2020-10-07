namespace AutomatedCar.Models
{
    using ReactiveUI;

    public abstract class WorldObject : ReactiveObject
    {
        private int _x;
        private int y;
        private double angle = 90; // mocking
        public int rotationCenterPointX = 90; // = width/2
        public int rotationCenterPointY = 120; // = height/2

        public WorldObject(int x, int y, string filename)
        {
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.ZIndex = 1;
        }

        public int ZIndex { get; set; }

        public double Angle
        {
            get => this.angle;
        }

        public int RotationCenterPointX
        {
            get => this.rotationCenterPointX;
        }

        public int RotationCenterPointY
        {
            get => this.rotationCenterPointY;
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
            get => this.y;
            set => this.RaiseAndSetIfChanged(ref this.y, value);
        }

        public string Filename { get; set; }

        public int Offset { get => 50; }
    }
}