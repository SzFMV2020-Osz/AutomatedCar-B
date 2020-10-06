namespace AutomatedCar.Models
{
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using ReactiveUI;

    public abstract class WorldObject : ReactiveObject
    {
        private int _x;
        private int _y;
        private double _angle;
        public int _rotationCenterPointX = 90; // = width/2
        public int _rotationCenterPointY = 120; // = height/2
        private bool visible = true;

        public WorldObject()
        {
            this.ZIndex = 0;
            this.Angle = 0;
            this.IsHighlighted = false;
        }

        public WorldObject(int x, int y, string filename): this()
        {
            this.X = x;
            this.Y = y;
            this.FileName = filename;
        }

        public int ZIndex { get; set; }
        public double Angle
        {
            get => this._angle;
            set => this._angle = value;
        }

        public int RotationCenterPointX
        {
            get => this._rotationCenterPointX;
        }
        public int RotationCenterPointY
        {
            get => this._rotationCenterPointY;
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public string FileName { get; protected set; }

        public Polygon Polygon { get; protected set; }
        
        public bool IsCollidable { get; protected set; }

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
        public bool Visible
        {
            get => this.visible;
            set => this.RaiseAndSetIfChanged(ref this.visible, value);
        }
    }
}