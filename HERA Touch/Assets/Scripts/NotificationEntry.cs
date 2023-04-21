using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using TMPro;

namespace HERATouch
{
    public class NotificationEntry : MonoBehaviour
    {
        public Notification notification;

        public TextMeshProUGUI timeStampText;

        public void Initialize(Notification a_notification)
        {
            notification = a_notification;

            GetComponentInChildren<NotificationText>().patientName = notification.patientName;
            GetComponentInChildren<NotificationText>().data = notification.data;

            timeStampText.text = string.Format("{0:00}:{1:00}", System.DateTime.Now.Hour, System.DateTime.Now.Minute);

            GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Notification", notification.localRefKey);
        }

    }
}
