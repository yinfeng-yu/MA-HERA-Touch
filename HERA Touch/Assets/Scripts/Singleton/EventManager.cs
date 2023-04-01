using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton
    public static EventManager instance;
    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }

        taskButtonEvents = new ExpandableEvents();
        taskBubbleEvents = new ExpandableEvents();
    }
    #endregion

    public ExpandableEvents taskButtonEvents;
    public ExpandableEvents taskBubbleEvents;

    public event Action pageChanged;
    public event Action<bool> settingsEntered;

    public void PageChanged()
    {
        pageChanged?.Invoke();
    }
    public void SettingsEntered(bool enter)
    {
        settingsEntered?.Invoke(enter);
    }

}
