using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Platform
{
    AR,
    Smartphone,
}

public class TransmissionManager : Singleton<TransmissionManager>
{
    /// <summary>
    /// The message transmitter using UDP over LAN; used for communicating with AR Headset
    /// </summary>
    public NetworkTransmitter NetworkTransmitter;

    public void SendTo(Message message, Platform platform)
    {
        switch (platform)
        {
            case Platform.AR:
                NetworkTransmitter.Send(message);
                break;
            case Platform.Smartphone:
                break;
            default:
                break;
        }
    }
        
}
