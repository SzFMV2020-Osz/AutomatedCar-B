using Avalonia;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AutomatedCar.Models
{
    public class NpcCar : WorldObject, INPC
    {
        public NpcCar(int x, int y, string filename, int width, int height, int referenceOffsetX, int referenceOffsetY, Matrix rotmatrix, List<List<Point>> polyPoints) : base(x, y, filename, width, height, referenceOffsetX, referenceOffsetY, rotmatrix, polyPoints)
        {
            
        }

        public int Rotation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void Move(Vector2 with)
        {
            this.X = (int)with.X;
            this.Y = (int)with.Y;
        }

        public void MoveX(int x)
        {
            throw new NotImplementedException();
        }

        public void MoveY(int y)
        {
            throw new NotImplementedException();
        }

        public void SetNextPosition(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
