namespace AutomatedCar.Models
{
    using System;
    using System.Reflection;
    using Avalonia.Media.Imaging;

    /// <summary>This is a dummy object for demonstrating the codebase.</summary>
public class Circle : WorldObject
{
    private Bitmap image;
    public Circle(int x, int y, string filename, int radius)
        : base(x, y, filename)
    {
        this.Radius = radius;
        image = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream($"AutomatedCar.Assets.WorldObjects.{filename}"));
    }

    public int Radius { get; set; }
    public Bitmap Image { get => this.image; }
    public double CalculateArea()
    {
        return Math.PI * this.Radius * this.Radius;
    }
}
}