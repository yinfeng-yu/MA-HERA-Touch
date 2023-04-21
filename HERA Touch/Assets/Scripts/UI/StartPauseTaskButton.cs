using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace HERATouch
{
    public class StartPauseTaskButton : MonoBehaviour
    {
        private void Start()
        {
            int index = GetComponentInParent<TasksListEntry>().taskIndex;
            if (AgentManager.instance.GetRobotAgent().GetTaskStatus(index) == TaskStatus.OnGoing)
            {
                // GetComponentInParent<TasksListEntry>().SetEdgeColor(true);
                GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", "[Task Bubble Option] Pause");
            }
            else
            {
                // GetComponentInParent<TasksListEntry>().SetEdgeColor(false);
                GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", "[Task Bubble Option] Start");
            }
        }

        public void OnClicked()
        {
            int index = GetComponentInParent<TasksListEntry>().taskIndex;
            if (AgentManager.instance.GetRobotAgent().GetTaskStatus(index) == TaskStatus.OnGoing)
            {
                // GetComponentInParent<TasksListEntry>().SetEdgeColor(false);
                AgentManager.instance.GetRobotAgent().PauseTask(GetComponentInParent<TasksListEntry>().taskIndex);
                GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", "[Task Bubble Option] Start");

                Debug.Log("Task Paused!");
            }
            else
            {
                // GetComponentInParent<TasksListEntry>().SetEdgeColor(true);
                AgentManager.instance.GetRobotAgent().StartTask(GetComponentInParent<TasksListEntry>().taskIndex);
                GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", "[Task Bubble Option] Pause");

                Debug.Log("Task Started!");
            }


            // GetComponentInParent<TasksListEntry>().ResetExpandable();
            EventManager.instance.taskBubbleEvents.Selected(false);


        }
    }

}
