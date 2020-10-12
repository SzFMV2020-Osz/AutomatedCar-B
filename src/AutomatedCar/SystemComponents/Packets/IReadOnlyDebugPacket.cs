public interface IReadOnlyDebugPacket
{
    bool UtrasoundSensor { get; }

    bool RadarSensor { get; }

    bool BoardCamera { get; }
}
