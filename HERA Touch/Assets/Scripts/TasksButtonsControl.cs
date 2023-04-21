using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public class TasksButtonsControl : MonoBehaviour
    {
        public GameObject tasksButtonsContainer;

        public Vector2 m_containerLerpTo;
        public bool m_containerLerp = false;

        public float snapSpeed = 5f;

        // Update is called once per frame
        void Update()
        {
            if (m_containerLerp)
            {
                if (Vector2.SqrMagnitude(tasksButtonsContainer.GetComponent<RectTransform>().anchoredPosition - m_containerLerpTo) < 0.25f)
                {
                    // snap to target and stop lerping
                    tasksButtonsContainer.GetComponent<RectTransform>().anchoredPosition = m_containerLerpTo;
                    m_containerLerp = false;
                }

                tasksButtonsContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(tasksButtonsContainer.GetComponent<RectTransform>().anchoredPosition,
                                                                                                    m_containerLerpTo,
                                                                                                    snapSpeed * Time.deltaTime);
                Debug.Log(tasksButtonsContainer.GetComponent<RectTransform>().anchoredPosition);
            }
        }

        public void LerpTo(float a_lerpToY)
        {
            m_containerLerp = true;
            m_containerLerpTo = new Vector2(tasksButtonsContainer.GetComponent<RectTransform>().anchoredPosition.x, Mathf.Max(0, 
                                                                                                                              Mathf.Min(a_lerpToY, 
                                                                                                                                        tasksButtonsContainer.GetComponent<RectTransform>().sizeDelta.y - GetComponent<RectTransform>().sizeDelta.y)));
        }
    }
}

