using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSender : MonoBehaviour
{
    public static CommandSender instance;
    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }
    }

    public Vector2 targetLocation;
    public Vector2 screenLocation;

    public List<Vector2> waypoints;
    public VisualizeWaypointsOnMap waypointsVisualizer;

    public void SendGrabCommand(bool isGrab, Handedness handedness)
    {
        Debug.Log("Send Grab Command");
        Command grabCommand = new Command(CommandType.Grab);
        grabCommand.isGrab = isGrab;
        grabCommand.handedness = handedness;
        TransmissionManager.Instance.SendTo(new CommandMessage(grabCommand), Platform.AR);
    }

    public void SendDisplaceCommand()
    {
        Debug.Log(targetLocation);
        Command displaceCommand = new Command(CommandType.Displace);
        displaceCommand.targetLocation = targetLocation;
        TransmissionManager.Instance.SendTo(new CommandMessage(displaceCommand), Platform.AR);
    }

    // Patrol command
    public void AddWaypoint()
    {
        waypoints.Add(targetLocation);
        waypointsVisualizer.AddWaypoint(screenLocation, waypoints.Count); // We start counting from 1 instead of 0, because this is more reader friendly!
    }

    public void SendPatrolCommand()
    {
        Command patrolCommand = new Command(CommandType.Patrol);
        patrolCommand.waypoints = waypoints.ToArray();

        if (waypoints.Count == 0)
        {
            waypointsVisualizer.ClearWaypoints();
        }
        waypoints.Clear();
        TransmissionManager.Instance.SendTo(new CommandMessage(patrolCommand), Platform.AR);
    }

    public void SendSwitchHandCommand(Handedness handedness)
    {
        Command switchHandCommand = new Command(CommandType.SwitchHand);
        switchHandCommand.handedness = handedness;
        TransmissionManager.Instance.SendTo(new CommandMessage(switchHandCommand), Platform.AR);
    }

    
}