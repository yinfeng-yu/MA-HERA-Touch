using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject smartPhoneControllerPage;


    // Update is called once per frame
    void Update()
    {
        if (currentControlMode == ControlMode.Smartphone || currentControlMode == ControlMode.SmartphonePointer)
        {
            smartPhoneControllerPage.SetActive(true);
        }
        else
        {
            smartPhoneControllerPage.SetActive(false);
        }
    }

    public void QuitToMainMenu()
    {
        currentControlMode = ControlMode.MainMenu;
    }
}
