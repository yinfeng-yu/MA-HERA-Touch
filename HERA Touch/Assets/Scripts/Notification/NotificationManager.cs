using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    #region Singleton
        public static NotificationManager instance;
        private void Awake()
        {
            if (instance != this)
            {
                instance = this;
            }
        }
        #endregion

    public List<Notification> notifications;

    public Transform entriesContainer;

    public GameObject newNotificationBubble;
    public GameObject noNotificationsPrompt;
    public TextMeshProUGUI roboyStateLabel;

    public GameObject listEntryPrefab;

    // Start is called before the first frame update
    void Start()
    {
        notifications = new List<Notification>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (bottomMenu.curToggleIndex == 1)
        // {
        //     newNotificationBubble.SetActive(false);
        // }

    }

    public void SilenceBubble()
    {
        newNotificationBubble.SetActive(false);
    }

    public void Notify(Notification notification)
    {
        if (notification.taskStatus == TaskStatus.Start)
        {
            roboyStateLabel.text = "Busy";
        }
        else if (notification.taskStatus == TaskStatus.End)
        {
            roboyStateLabel.text = "Standing by";

            notifications.Add(notification);
            UpdateDisplay();

            // Debug.Log($"notification time: {notification.timeStamp.hour}:{notification.timeStamp.minute}:{notification.timeStamp.second}");
            newNotificationBubble.SetActive(true);
        }
        
    }

    void UpdateDisplay()
    {
        foreach (Transform entry in entriesContainer)
        {
            Destroy(entry.gameObject);
        }

        foreach (Notification notification in notifications)
        {
            var go = Instantiate(listEntryPrefab, entriesContainer);
            go.GetComponent<NotificationsListEntry>().Initialize(notification);
        }

        noNotificationsPrompt.SetActive(notifications.Count == 0 ? true : false);
    }

    public void ClearNotifications()
    {
        notifications.Clear();
        UpdateDisplay();
    }

    public void ClearNotification(Notification notification)
    {
        notifications.Remove(notification);
        UpdateDisplay();
    }

    // private string GetLocalRefKey(TaskType a_taskType)
    // {
    //     switch (a_taskType)
    //     {
    //         case TaskType.MeasureTemperature:
    //             return "[Notification List Entry] Temperature";
    //         case TaskType.BringWater:
    //             return "[Notification List Entry] Water";
    //         case TaskType.MoveToSite:
    //             return "[Notification List Entry] Move";
    //         default:
    //             return "";
    //     }
    // }
            
}
