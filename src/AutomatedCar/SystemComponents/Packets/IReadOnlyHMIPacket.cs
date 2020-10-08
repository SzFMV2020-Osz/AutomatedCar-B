public interface IReadOnlyHMIPacket
{
    double Gaspedal { get; }

    double Breakpedal { get; }

    double Steering { get; }

    Gears Gear { get; }

    bool ACC { get; }

    double ACCDistance { get; }

    int ACCSpeed { get; }

    bool LaneKeeping { get; }

    bool ParkingPilot { get; }

    bool TurnSignalRight { get; }

    bool TurnSignalLeft { get; }

    string Sign { get; }
}