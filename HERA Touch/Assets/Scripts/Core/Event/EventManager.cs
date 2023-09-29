using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    // Notification Events
    public event Action<string> NotificationReceived;

    // Video Stream Events
    public event Action<StreamDataHeader, byte[]> StreamUpdated;
    public void InvokeStreamUpdated(StreamDataHeader header, byte[] data) => StreamUpdated?.Invoke(header, data);


    public ExpandableEvents taskButtonEvents;
    public ExpandableEvents taskBubbleEvents;

    public event Action pageChanged;
    public event Action<bool> settingsEntered;
    public event Action<bool, bool> notificationEntered;

}
