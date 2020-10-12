namespace AutomatedCar.Models
{
    using Avalonia.Controls.Shapes;

    public class Tree : WorldObject
    {
        public Tree(int x, int y, string filename, bool iscolliding, RotationMatrix rotmatrix, Polygon wood)
            : base(x, y, filename, iscolliding, rotmatrix)
        {
            this.Polygons.Add(wood);
            this.ZIndex = 3;
        }
    }
}