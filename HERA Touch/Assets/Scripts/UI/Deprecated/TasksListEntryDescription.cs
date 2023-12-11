using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HERATouch
{
    public class TasksListEntryDescription : MonoBehaviour
    {

        public void UpdateText(string text)
        {
            string tagFront = "<color=orange>";
            string tagBack = "</color>";
            string targetSiteName = "";
            string updatedText;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '{')
                {
                    updatedText = text.Substring(0, i) + tagFront + targetSiteName + tagBack + text.Substring(i + 2, text.Length - i - 2);
                    GetComponent<TextMeshProUGUI>().text = updatedText;
                    return;
                }
            }

            GetComponent<TextMeshProUGUI>().text = text;
        }
    }

}
