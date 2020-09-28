namespace AutomatedCar.Models
{
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using ReactiveUI;

    public abstract class WorldObject : ReactiveObject
    {
        private int _x;
        private int _y;

        public WorldObject(int x, int y, string filename)
        {
            this.X = x;
            this.Y = y;
            this.Filename = filename;
            this.ZIndex = 1;
        }

        public int ZIndex { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string FileName { get; set; }

        public Point PositionPoint { get; set; }

        public Point RotationPoint { get; set; }

        public Polygon Polygon { get; set; }

        public bool IsCollidable { get; }

        public MatrixTwoByTwo RotationMatrix { get; set; }

        public bool IsHighlighted { get; set; }

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