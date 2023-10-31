using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This enum is defined the same way as that of AR Environment
/// </summary>
public enum Handedness
{
    Left,
    Right,
    Both,
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
        // pressed = true;
        CommandSender.instance.SendGrabCommand(true, handedness);

        Handheld.Vibrate();
    }

    public void GrabButtonPressEnd()
    {
        // pressed = false;
        // holdTime = 0f;
        // 
        // switch (handedness)
        // {
        //     case Handedness.Left:
        //         leftGrabButton.transform.localScale = new Vector3(1f, 1f, 1f);
        //         break;
        //     case Handedness.Right:
        //         rightGrabButton.transform.localScale = new Vector3(1f, 1f, 1f);
        //         break;
        //     default:
        //         break;
        // }

        CommandSender.instance.SendGrabCommand(false, handedness);
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

        // CommandSender.instance.SendGrabCommand(handedness);
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
