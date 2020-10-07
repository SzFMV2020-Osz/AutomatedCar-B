namespace AutomatedCar.Models
{
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using ReactiveUI;

    public class WorldObject : ReactiveObject
    {
        private int _x;
        private int _y;

        private int _visibleX;
        private int _visibleY;

        private double _angle;

        public int _rotationCenterPointX = 90; // = width/2
        public int _rotationCenterPointY = 120; // = height/2

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
        
        public double Angle { get; set; }

        public int RotationCenterPointX 
        {
             get => this._rotationCenterPointX;
             set => this._rotationCenterPointX = value;
        }

        public int RotationCenterPointY
        {
             get => this._rotationCenterPointY;
             set => this._rotationCenterPointY = value;
        }

        private int _width;
        public int Width
        {
            get => this._width;
            set => this.RaiseAndSetIfChanged(ref this._width, value);
        }

        private int _height;
        public int Height
        {
            get => this._height;
            set => this.RaiseAndSetIfChanged(ref this._height, value);
        }

        public string FileName { get; set; }

        public Polygon[] Polygon { get; set; }
        
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


        public int VisibleX
        {
            get => this._visibleX;
            set => this.RaiseAndSetIfChanged(ref this._visibleX, value);
        }

        public int VisibleY
        {
            get => this._visibleY;
            set => this.RaiseAndSetIfChanged(ref this._visibleY, value);
        }

        public string Filename { get; set; }

    }
}