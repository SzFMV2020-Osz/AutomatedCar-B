public interface IReadOnlyHMIPacket
{
    double Gaspedal { get; }

    double Breakpedal { get; }

    double Steering { get; }

    Gears Gear { get; }

    bool Acc { get; }

    double AccDistance { get; }

    int AccSpeed { get; }

    bool LaneKeeping { get; }

    bool ParkingPilot { get; }

    bool TurnSignalRight { get; }

    bool TurnSignalLeft { get; }

    string Sign { get; }
}