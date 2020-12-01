namespace AutomatedCar.Models
{
    using System.Numerics;
    using Avalonia.Media;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using SystemComponents;
    using Avalonia;
    using ReactiveUI;
    using System;
    using System.Linq;
    using static global::AutomatedCar.Models.World;

    public class AutomatedCar : WorldObject, IMoveable
    {
        private VirtualFunctionBus virtualFunctionBus;
        private DummySensor dummySensor;
        private HumanMachineInterface humanMachineInterface;
        private PowerTrain powerTrain;
        private Ultrasound[] ultrasounds;

        public ObservableCollection<DummySensor> Sensors { get; } = new ObservableCollection<DummySensor>();

        /*public AutomatedCar(int x, int y, string filename)
            : base(x, y, filename, true,  new RotationMatrix(1.0, 0.0, 0.0, 1.0))*/
        public AutomatedCar(int x, int y, string filename, int width, int height, List<List<Point>> polylist)
            : base(x, y, filename, width, height, -width / 2, -height / 2, new Matrix(1, 0, 0, 1, 1, 1), polylist)
        {
            this.virtualFunctionBus = new VirtualFunctionBus();
            this.humanMachineInterface = new HumanMachineInterface(this.virtualFunctionBus);
            this.powerTrain = new PowerTrain(this.virtualFunctionBus);
            this.dummySensor = new DummySensor(this.virtualFunctionBus);
            this.Brush = new SolidColorBrush(Color.Parse("red"));
            this.Ultrasounds = new Ultrasound[]
            {
                new Ultrasound(this.virtualFunctionBus, 110, 30, 0),
                new Ultrasound(this.virtualFunctionBus, 105, 45, 90),
                new Ultrasound(this.virtualFunctionBus, 110, -30, 0),
                new Ultrasound(this.virtualFunctionBus, 105, -45, -90),
                new Ultrasound(this.virtualFunctionBus, -120, 25, 180),
                new Ultrasound(this.virtualFunctionBus, -105, 45, 90),
                new Ultrasound(this.virtualFunctionBus, -120, -25, 180),
                new Ultrasound(this.virtualFunctionBus, -105, -45, -90),
            };

            this.ultraSoundVisible = true;
            this.radarVisible = true;
            this.cameraVisible = true;
        }

        public VirtualFunctionBus VirtualFunctionBus { get => this.virtualFunctionBus; }

        public HumanMachineInterface HumanMachineInterface { get => this.humanMachineInterface; }

        public Ultrasound[] Ultrasounds { get => this.ultrasounds; set { this.RaiseAndSetIfChanged(ref this.ultrasounds, value); } }

        public Ultrasound Ultrasound0 { get => this.ultrasounds[0]; set { this.RaiseAndSetIfChanged(ref this.ultrasounds[0], value); } }

        public Geometry Geometry { get; set; }

        public SolidColorBrush Brush { get; private set; }

        public int Speed { get; set; }

        public int Mass { get; set; } = 5;

        public void SetNextPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void Move(Vector2 newPosition)
        {
            var crashedObjects = GetCrashedObjects();
            newPosition = this.CrashEffects(newPosition, crashedObjects);
            this.X = (int)newPosition.X;
            this.Y = (int)newPosition.Y;

            //LaneKeepingAssistantFunction();
        }

        private static List<WorldObject> GetCrashedObjects()
        {
            return World.Instance.GetWorldObjectsInsideTriangle(World.Instance.ControlledCar.NetPolygons[0].Coordinates.Select(x => new Point(x.X, x.Y)).ToList());
        }

        private Vector2 CrashEffects(Vector2 newPosition, List<WorldObject> crashed)
        {
            foreach (var col in crashed)
            {
                if (col is Tree)
                {
                    newPosition = this.TreeCrash(newPosition);
                }

                if (col is Sign)
                {
                    newPosition = this.SignCrash(newPosition, col);
                }
            }

            return newPosition;
        }

        private Vector2 SignCrash(Vector2 newPosition, WorldObject col)
        {
            var carSpeed = this.CalcCarSpeedVec(newPosition);
            this.CrashDamage(newPosition, 2f, carSpeed);
            this.ImpactOnSign(col, carSpeed);
            newPosition = this.ImpactOnCar(newPosition);
            return newPosition;
        }

        private Vector2 ImpactOnCar(Vector2 newPosition)
        {
            var carPos = new Vector2(this.X, this.Y);
            var impact = new Vector2(this.X - newPosition.X, this.Y - newPosition.Y);
            impact = Vector2.Multiply(2, impact);
            var effectedPos = Vector2.Add(carPos, impact);
            newPosition.X = effectedPos.X;
            newPosition.Y = effectedPos.Y;
            return newPosition;
        }

        private void ImpactOnSign(WorldObject col, Vector2 carSpeed)
        {
            var carMomentum = Vector2.Multiply(Mass, carSpeed);
            var signVelocity = Vector2.Multiply(1, carMomentum);
            Sign sign = (col as Sign);
            var singPosition = new Vector2(sign.X, sign.Y);
            var res = Vector2.Add(signVelocity, singPosition);
            sign.SetNextPosition((int) res.X, (int) res.Y);
        }

        private Vector2 TreeCrash(Vector2 newPosition)
        {
            this.CrashDamage(newPosition, 2f, CalcCarSpeedVec(newPosition));

            // === Backup for demo ===
            // var impact = new Vector2(( this.X - newPosition.X ), ( this.Y - newPosition.Y) );
            // impact = Vector2.Multiply(2, impact);
            // var carPos = new Vector2(this.X, this.Y);
            // var effectedPos = Vector2.Add(carPos, impact);
            // newPosition.X = effectedPos.X;
            // newPosition.Y = effectedPos.Y;
            // === swap comment on lines below with lines above ===
            newPosition.X = this.X;
            newPosition.Y = this.Y;

            // Speed = 0;
            return newPosition;
        }

        private Vector2 CalcCarSpeedVec(Vector2 newPosition)
        {
            return new Vector2((newPosition.X - this.X), (newPosition.Y - this.Y));
        }

        private void CrashDamage(Vector2 newPosition, float factor, Vector2 carSpeed)
        {
            World.Instance.ControlledCar.DamageOnCollision(Vector2.Multiply(factor, carSpeed), new Vector2(0, 0));
        }

        /// <summary>Starts the automated cor by starting the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public SolidColorBrush RadarBrush { get; set; }

        public Geometry RadarGeometry { get; set; }

        private bool radarVisible;

        public bool RadarVisible
        {
            get => radarVisible;
            set => this.RaiseAndSetIfChanged(ref radarVisible, value);
        }

        private bool ultraSoundVisible;

        public bool UltraSoundVisible
        {
            get => ultraSoundVisible;
            set => this.RaiseAndSetIfChanged(ref ultraSoundVisible, value);
        }

        public SolidColorBrush CameraBrush { get; set; }

        public PolylineGeometry CameraGeometry { get; set; }

        private bool cameraVisible;

        public bool CameraVisible
        {
            get => cameraVisible;
            set => this.RaiseAndSetIfChanged(ref cameraVisible, value);
        }

        public int VisibleX { get; set; }

        public int VisibleY { get; set; }

        private int healthPoints = 100;
        public int HealthPoints
        {
            get => healthPoints;
            set => this.RaiseAndSetIfChanged(ref healthPoints, value);
        }

        /// <summary>
        /// Damages the car. The amount of damage is dependant on the momentary velocity vector of the car and the other object that was hit.
        /// </summary>
        /// <param name="carVector">The velocity vector of the car at the time of the impact</param>
        /// <param name="otherObjectVector">The velocity vector of the other object involved in the collision.</param>
        public void DamageOnCollision(Vector2 carVector, Vector2 otherObjectVector)
        {
            this.healthPoints -= Math.Abs(((int)carVector.X + (int)carVector.Y) - ((int)otherObjectVector.X + (int)otherObjectVector.Y));
        }

        /// <summary>Stops the automated car by stopping the ticker in the Virtual Function Bus, that cyclically calls the system components.</summary>
        public void Stop()
        {
            this.virtualFunctionBus.Stop();
        }

        public void Start()
        {
            this.virtualFunctionBus.Start();
        }

        public void LaneKeepingAssistantFunction()
        {
            if (virtualFunctionBus.HMIPacket.LaneKeeping)
            {
                NetTopologySuite.Geometries.Coordinate coordinate = isCarInMidleOfRoad();

                if (coordinate != null)
                {
                    VirtualFunctionBus.LaneKeepingPacket.Active = true;

                    VirtualFunctionBus.LaneKeepingPacket.Steering = 10 * CalculateDirection(coordinate);
                }
                else {
                    VirtualFunctionBus.LaneKeepingPacket.Active = false;
                }
            }
            else 
            {
                VirtualFunctionBus.LaneKeepingPacket.Active = false;
            }
        }

        private NetTopologySuite.Geometries.Coordinate isCarInMidleOfRoad() 
        {
            
            Road road = World.Instance.GetRoadUnderCar();
            NetTopologySuite.Geometries.Coordinate[] coordinates = TwoclosestRoadPoligonCoordinates(road);

            double c1_d = Math.Sqrt(Math.Pow(this.X - coordinates[0].X, 2) + Math.Pow(this.Y - coordinates[0].Y, 2));

            double c2_d = Math.Sqrt(Math.Pow(this.X - coordinates[1].X, 2) + Math.Pow(this.Y - coordinates[1].Y, 2));

            if (c1_d > c2_d)
            {
                return coordinates[0];
            }
            else if (c1_d < c2_d)
            {
                return coordinates[1];
            }
            else
            {
                return null;
            }
        }

        private NetTopologySuite.Geometries.Coordinate[] TwoclosestRoadPoligonCoordinates(Road road) {

            List<KeyValuePair<NetTopologySuite.Geometries.LineString, double>> r_d = new List<KeyValuePair<NetTopologySuite.Geometries.LineString, double>>();

            foreach (var item in road.NetPolygons)
            {
                foreach (var item2 in item.Coordinates)
                {
                    r_d.Add(new KeyValuePair<NetTopologySuite.Geometries.LineString, double>( item, Math.Sqrt(Math.Pow(this.X - item2.X, 2) + Math.Pow(this.Y - item2.Y, 2))));
                }
            }

            r_d.OrderByDescending(x => x.Value);


            List<KeyValuePair<NetTopologySuite.Geometries.Coordinate, double>> coordinates1 = new List<KeyValuePair<NetTopologySuite.Geometries.Coordinate, double>>();

            foreach (var item2 in r_d[0].Key.Coordinates)
            {
                coordinates1.Add(new KeyValuePair<NetTopologySuite.Geometries.Coordinate, double>(item2,Math.Sqrt(Math.Pow(this.X - item2.X, 2) + Math.Pow(this.Y - item2.Y, 2))));
            }

            coordinates1.OrderByDescending(x => x.Value);

            List<KeyValuePair<NetTopologySuite.Geometries.Coordinate, double>> coordinates2 = new List<KeyValuePair<NetTopologySuite.Geometries.Coordinate, double>>();

            foreach (var item2 in r_d[1].Key.Coordinates)
            {
                coordinates2.Add(new KeyValuePair<NetTopologySuite.Geometries.Coordinate, double>(item2, Math.Sqrt(Math.Pow(this.X - item2.X, 2) + Math.Pow(this.Y - item2.Y, 2))));
            }

            coordinates2.OrderByDescending(x => x.Value);

            return new NetTopologySuite.Geometries.Coordinate[] { coordinates1[0].Key, coordinates2[1].Key };
        }

        private int CalculateDirection(NetTopologySuite.Geometries.Coordinate coordinate) {
            if (Angle > 315 || Angle < 45)
            {
                return Math.Sign(coordinate.X - this.X);
            }
            else if (Angle >= 45 && Angle < 135)
            {
                return Math.Sign(coordinate.Y - this.Y);
            }
            else if (Angle >= 135 && Angle < 225)
            {
                return Math.Sign(this.X - coordinate.X);
            }
            else
            {
                return Math.Sign(this.Y - coordinate.Y);
            }
        }
    }
}