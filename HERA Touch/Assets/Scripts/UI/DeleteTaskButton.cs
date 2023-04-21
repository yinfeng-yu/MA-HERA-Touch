using System;
using UnityEngine.EventSystems;
using UnityEngine;

namespace HERATouch
{
    public class DeleteTaskButton : MonoBehaviour
    {
        public void OnClicked()
        {
            AgentManager.instance.GetRobotAgent().DeleteTask(GetComponentInParent<TasksListEntry>().taskIndex);
        }

    }
}

