using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CommandType
{
    Grab,
    Displace,
    Patrol,
    SwitchHand,
    Steer,
}

public enum Direction
{
    None,
    Forward,
    Back,
    Left,
    Right,
}

// Unity serialization does not support derived classes.
// As the result, the base class has to contain everything.
[Serializable]
public class Command
{
    public CommandType type;

    // Grab command
    public Handedness handedness;

    // Displace command
    public Vector2 targetLocation;

    // Patrol waypoints
    public Vector2[] waypoints;

    public Direction direction;

    public Command(CommandType a_commandType)
    {
        type = a_commandType;
    }
}