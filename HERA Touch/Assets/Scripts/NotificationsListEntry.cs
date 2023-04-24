using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using TMPro;
using UnityEngine.EventSystems;

namespace HERATouch
{
    public class NotificationsListEntry : MonoBehaviour, IPointerExitHandler
    {
        public Notification notification;

        public TextMeshProUGUI timeStampText;

        public void Initialize(Notification a_notification)
        {
            notification = a_notification;

            timeStampText.text = string.Format("{0:00}:{1:00}", System.DateTime.Now.Hour, System.DateTime.Now.Minute);

            GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Notification", notification.localRefKey);
        }

        public void ShowClearButton()
        {
            if (!GetComponent<Animator>().GetBool("movedLeft"))
            {
                GetComponent<Animator>().SetBool("movedLeft", true);
            }
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (GetComponent<Animator>().GetBool("movedLeft"))
            {
                GetComponent<Animator>().SetBool("movedLeft", false);
            }

        }

        public void ClearThisNotification()
        {
            NotificationManager.instance.ClearNotification(notification);
        }

    }
}
