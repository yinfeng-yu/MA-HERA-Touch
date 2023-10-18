using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Direction
{
    None = 0,
    Forward,
    Back,
    Left,
    Right,
}

public class OperationSender : MonoBehaviour
{

    public void SendConfirm()
    {
        Debug.Log("Confirm");
        Operation operation = new Operation();
        operation.type = OperationType.Confirm;
        operation.confirm = true;

        // TransmissionManager.instance.Send(new BaseControlMessage(baseControlInfo));
        // Transmitter.Instance.Send(new BaseControlMessage(baseControlInfo));
        TransmissionManager.Instance.SendTo(new OperationMessage(operation), Platform.AR);
    }

    public void SendSteer(int direction)
    {
        Operation operation = new Operation();
        operation.type = OperationType.Steer;
        operation.direction = (Direction)direction;

        TransmissionManager.Instance.SendTo(new OperationMessage(operation), Platform.AR);
    }

    public void SendSwitchToMainMenu()
    {
        Operation operation = new Operation();
        operation.type = OperationType.SwitchControlMode;
        operation.controlMode = ControlMode.MainMenu;

        TransmissionManager.Instance.SendTo(new OperationMessage(operation), Platform.AR);
    }

    public void SendFreeze(bool isFrozen)
    {
        Operation operation = new Operation();
        operation.type = OperationType.Freeze;
        operation.isFrozen = isFrozen;

        TransmissionManager.Instance.SendTo(new OperationMessage(operation), Platform.AR);
    }
}
