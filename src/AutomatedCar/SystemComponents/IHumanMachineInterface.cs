using AutomatedCar.SystemComponents.Packets;

interface IHumanMachineInterface
{
    HMIPacket hmiPacket { get; }

    DebugPacket debugPacket { get; }

    bool GaspedalDown { get; set; }

    bool BreakpedalDown { get; set; }

    bool SteeringRight { get; set; }

    bool SteeringLeft { get; set; }

    bool GearUp { get; set; }

    bool GearDown { get; set; }

    bool Acc { get; set; }

    bool AccDistance { get; set; }

    bool AccSpeedPlus { get; set; }

    bool AccSpeedMinus { get; set; }

    bool LaneKeeping { get; set; }

    bool ParkingPilot { get; set; }

    bool TurnSignalRight { get; set; }

    bool TurnSignalLeft { get; set; }

    bool UtrasoundDebug { get; set; }

    bool RadarDebug { get; set; }

    bool CameraDebug { get; set; }
}