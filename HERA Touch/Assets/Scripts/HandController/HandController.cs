using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This enum is defined the same way as that of MRTK
/// </summary>
public enum Handedness : byte
{
    /// <summary>
    /// No hand specified by the SDK for the controller
    /// </summary>
    None = 0 << 0,
    /// <summary>
    /// The controller is identified as being provided in a Left hand
    /// </summary>
    Left = 1 << 0,
    /// <summary>
    /// The controller is identified as being provided in a Right hand
    /// </summary>
    Right = 1 << 1,
    /// <summary>
    /// The controller is identified as being either left and/or right handed.
    /// </summary>
    Both = Left | Right,
    /// <summary>
    /// Reserved, for systems that provide alternate hand state.
    /// </summary>
    Other = 1 << 2,
    /// <summary>
    /// Global catchall, used to map actions to any controller (provided the controller supports it)
    /// </summary>
    /// <remarks>Note, by default the specific hand actions will override settings mapped as both</remarks>
    Any = Other | Left | Right,
}

public class HandController : MonoBehaviour
{
    public float holdDuration = 1f;
    public float holdTime = 0f;
    public bool pressed = false;

    public GameObject leftHandControl;
    public GameObject rightHandControl;

    public GameObject leftGrabButton;
    public GameObject rightGrabButton;

    public Handedness handedness = Handedness.Left;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            holdTime += Time.deltaTime;

            switch (handedness)
            {
                case Handedness.Left:
                    leftGrabButton.transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(1.7f, 1.7f, 1f), holdTime / holdDuration);
                    break;
                case Handedness.Right:
                    rightGrabButton.transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), new Vector3(1.7f, 1.7f, 1f), holdTime / holdDuration);
                    break;
                default:
                    break;
            }
        }

        if (holdTime >= holdDuration)
        {
            pressed = false;
            holdTime = 0f;

            GrabButtonTriggered();
        }
    }

    public void GrabButtonPressStart()
    {
        pressed = true;

        
        Handheld.Vibrate();
    }

    public void GrabButtonPressEnd()
    {
        pressed = false;
        holdTime = 0f;

        switch (handedness)
        {
            case Handedness.Left:
                leftGrabButton.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case Handedness.Right:
                rightGrabButton.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            default:
                break;
        }
    }

    public void GrabButtonTriggered()
    {
        Handheld.Vibrate();

        switch (handedness)
        {
            case Handedness.Left:
                leftGrabButton.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            case Handedness.Right:
                rightGrabButton.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            default:
                break;
        }

        CommandSender.instance.SendGrabCommand(handedness);
    }

    public void SwitchHand()
    {
        handedness = handedness ^ Handedness.Both;

        switch (handedness)
        {
            case Handedness.Left:
                leftHandControl.SetActive(true);
                rightHandControl.SetActive(false);
                break;
            case Handedness.Right:
                leftHandControl.SetActive(false);
                rightHandControl.SetActive(true);
                break;
            default:
                break;
        }

        CommandSender.instance.SendSwitchHandCommand(handedness);
    }
}
