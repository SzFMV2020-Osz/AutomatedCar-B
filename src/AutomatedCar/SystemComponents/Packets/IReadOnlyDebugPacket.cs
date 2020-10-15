public interface IReadOnlyDebugPacket
{
    bool Polygon { get; }

    bool UtrasoundSensor { get; }

    bool RadarSensor { get; }

    bool BoardCamera { get; }
}
