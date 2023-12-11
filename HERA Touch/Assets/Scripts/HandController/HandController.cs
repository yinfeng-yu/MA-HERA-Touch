using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This enum is defined the same way as that of AR Environment project
/// </summary>
public enum Handedness
{
    Left,
    Right,
    Both,
}

public class HandController : Singleton<HandController>
{
    public GameObject leftHandControl;
    public GameObject rightHandControl;

    public Handedness handedness = Handedness.Left;

    public void GrabButtonPressStart()
    {
        Handheld.Vibrate();
        CommandSender.instance.SendGrabCommand(true, handedness);
    }
    
    public void GrabButtonPressEnd()
    {
        Handheld.Vibrate();
        CommandSender.instance.SendGrabCommand(false, handedness);
    }

    public void SwitchHand()
    {
        // Bit hack is also an option
        if (handedness == Handedness.Left)
        {
            handedness = Handedness.Right;
        }
        else
        {
            handedness = Handedness.Left;
        }

        // Command the AR Environment to switch hand
        CommandSender.instance.SendSwitchHandCommand(handedness);
    }
}
