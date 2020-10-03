namespace AutomatedCar.Models
{
    using Avalonia;
    using Avalonia.Controls.Shapes;
    using ReactiveUI;

    public class WorldObject : ReactiveObject
    {
        private int _x;
        private int _y;
        //private double _angle = 90; // mocking
        public int _rotationCenterPointX = 90; // = width/2
        public int _rotationCenterPointY = 120; // = height/2

        public WorldObject()
        {

        }

        public WorldObject(int x, int y, string filename)
        {
            this.X = x;
            this.Y = y;
            this.FileName = filename;
            this.ZIndex = 1;
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

        public int Width { get; set; }

        public int Height { get; set; }

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

       
    }
}