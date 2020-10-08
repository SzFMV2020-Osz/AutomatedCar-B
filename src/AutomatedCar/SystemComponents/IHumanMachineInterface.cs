using AutomatedCar.SystemComponents.Packets;

interface IHumanMachineInterface
{
    HMIPacket hmiPacket { get; }

    DebugPacket debugPacket { get; }

    bool GaspedalDown { get; set; }

    bool BreakpedalDown { get; set; }

    bool SteeringRight { get; set; }

    bool SteeringLeft { get; set; }

    bool GeerUp { get; set; }

    bool GeerDown { get; set; }

    bool ACC { get; set; }

    bool ACCDistance { get; set; }

    bool ACCSpeedPus { get; set; }

    bool ACCSpeedMinus { get; set; }

    bool LaneKeeping { get; set; }

    bool ParkingPilot { get; set; }

    bool TurnSignalRight { get; set; }

    bool TurnSignalLeft { get; set; }

    bool UtrasoundDebug { get; set; }

    bool RadarDebug { get; set; }

    bool CameraDebug { get; set; }
}