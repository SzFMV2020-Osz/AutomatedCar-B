using AutomatedCar.SystemComponents.Packets;

public interface IHumanMachineInterface
{
    HMIPacket HmiPacket { get; }

    bool GaspedalDown { get; }

    bool BreakpedalDown { get; }

    bool SteeringRight { get; }

    bool SteeringLeft { get; }

    bool GearUp { get; }

    bool GearDown { get; }

    bool Acc { get; }

    bool AccDistance { get; }

    bool AccSpeedPlus { get; }

    bool AccSpeedMinus { get; }

    bool LaneKeeping { get; }

    bool ParkingPilot { get; }

    bool TurnSignalRight { get; }

    bool TurnSignalLeft { get; }

    bool UltrasoundDebug { get; }

    bool RadarDebug { get; }

    bool CameraDebug { get; }

    bool PolygonDebug { get; }
}