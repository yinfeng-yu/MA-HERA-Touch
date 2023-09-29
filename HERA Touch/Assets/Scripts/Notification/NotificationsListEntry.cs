using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using TMPro;
using UnityEngine.EventSystems;

public class NotificationsListEntry : MonoBehaviour, IPointerExitHandler
{
    public Notification notification;

    public TextMeshProUGUI timeStampText;
    public TextMeshProUGUI notificationText;

    public void Initialize(Notification a_notification)
    {
        notification = a_notification;

        timeStampText.text = string.Format("{0:00}:{1:00}:{2:00}", a_notification.timeStamp.hour, a_notification.timeStamp.minute, a_notification.timeStamp.second);
        notificationText.text = a_notification.ProduceContent();
        // GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Notification", notification.localRefKey);
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