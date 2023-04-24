using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HERATouch
{
    public class NotificationText : MonoBehaviour
    {
        public void UpdateText(string text)
        {
            string nameTagFront = "<color=orange>";
            string dataTagFront = "<color=green>";
            string tagBack = "</color>";

            string taskSiteEnum = SitesManager.instance.GetSiteName(GetComponentInParent<NotificationsListEntry>().notification.taskSiteEnum);
            string data = GetComponentInParent<NotificationsListEntry>().notification.data;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '{')
                {
                    string updatedText = text.Substring(0, i) + nameTagFront + taskSiteEnum + tagBack + text.Substring(i + 2, text.Length - i - 2);
                    GetComponent<TextMeshProUGUI>().text = updatedText;
                }
            }

            string curText = GetComponent<TextMeshProUGUI>().text;
            for (int i = 0; i < curText.Length; i++)
            {
                if (curText[i] == '[')
                {
                    string updatedText = curText.Substring(0, i) + dataTagFront + data + tagBack + curText.Substring(i + 2, curText.Length - i - 2);
                    GetComponent<TextMeshProUGUI>().text = updatedText;
                }
            }

        }
    }

}
