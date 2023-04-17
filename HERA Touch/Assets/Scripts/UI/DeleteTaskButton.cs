using System;
using UnityEngine.EventSystems;
using UnityEngine;

namespace HERATouch
{
    public class DeleteTaskButton : MonoBehaviour
    {
        public void OnClicked()
        {
            AgentManager.instance.GetRobotAgent().DeleteTask(GetComponentInParent<TaskBubble>().taskIndex);
            EventManager.instance.taskBubbleEvents.Selected(false);
            // Debug.Log("Task Deleted!");
        }

    }
}

