using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
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

        public Transform notificationsContainer;
        public ToggleMenu bottomMenu;

        public GameObject notificationDot;
        public GameObject noNotificationText;

        public GameObject entryPrefab;

        // Start is called before the first frame update
        void Start()
        {
            notifications = new List<Notification>();
        }

        // Update is called once per frame
        void Update()
        {
            if (bottomMenu.curToggleIndex == 1)
            {
                notificationDot.SetActive(false);
            }

            noNotificationText.SetActive(notifications.Count == 0 ? true : false);
        }

        public void Notify(TaskType a_taskType, string a_patientName, string a_data)
        {
            Notification notification = new Notification(a_taskType, a_patientName, a_data, GetLocalRefKey(a_taskType));
            notifications.Add(notification);

            if (bottomMenu.curToggleIndex != 1)
            {
                notificationDot.SetActive(true);
            }

            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            foreach (Transform entry in notificationsContainer)
            {
                Destroy(entry.gameObject);
            }

            foreach (Notification notification in notifications)
            {
                var go = Instantiate(entryPrefab, notificationsContainer);
                go.GetComponent<NotificationEntry>().Initialize(notification);
            }
        }

        public void ClearNotifications()
        {
            notifications.Clear();
            UpdateDisplay();
        }

        private string GetLocalRefKey(TaskType a_taskType)
        {
            switch (a_taskType)
            {
                case TaskType.MeasureTemperature:
                    return "Temperature";
                case TaskType.BringWater:
                    return "Water";
                default:
                    return "";
            }
        }
                
    }

}
