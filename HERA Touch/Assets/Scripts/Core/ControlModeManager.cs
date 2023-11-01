using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[Serializable]
public enum ControlMode
{
    MainMenu,
    Smartphone,
    SmartphonePointer,
    SmartphonePoseCalibration,
    HandTracking,
    Steer,
}

public class ControlModeManager : Singleton<ControlModeManager>
{
    public ControlMode currentControlMode;
    public GameObject smartPhoneControllerPagePointer;
    public GameObject smartPhoneControllerPageMotionTracking;
    public GameObject smartPhoneControllerPageSteer;


    // Update is called once per frame
    void Update()
    {
        if (currentControlMode == ControlMode.SmartphonePointer)
        {
            smartPhoneControllerPagePointer.SetActive(true);
        }
        else if (currentControlMode == ControlMode.Smartphone)
        {
            smartPhoneControllerPageMotionTracking.SetActive(true);
        }
        else if (currentControlMode == ControlMode.Steer)
        {
            smartPhoneControllerPageSteer.SetActive(true);
        }
        else
        {
            smartPhoneControllerPageMotionTracking.SetActive(false);
            smartPhoneControllerPagePointer.SetActive(false);
            smartPhoneControllerPageSteer.SetActive(false);
        }
    }

    public void QuitToMainMenu()
    {
        currentControlMode = ControlMode.MainMenu;
    }
}
