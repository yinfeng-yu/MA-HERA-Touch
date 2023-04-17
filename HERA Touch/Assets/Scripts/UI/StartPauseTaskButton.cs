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
            int index = GetComponentInParent<TaskBubble>().taskIndex;
            if (AgentManager.instance.GetRobotAgent().GetTaskStatus(index) == TaskStatus.OnGoing)
            {
                GetComponentInParent<TaskBubble>().SetEdgeColor(true);
                GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", "[Task Bubble Option] Pause");
            }
            else
            {
                GetComponentInParent<TaskBubble>().SetEdgeColor(false);
                GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", "[Task Bubble Option] Start");
            }
        }

        public void OnClicked()
        {
            int index = GetComponentInParent<TaskBubble>().taskIndex;
            if (AgentManager.instance.GetRobotAgent().GetTaskStatus(index) == TaskStatus.OnGoing)
            {
                GetComponentInParent<TaskBubble>().SetEdgeColor(false);
                AgentManager.instance.GetRobotAgent().PauseTask(GetComponentInParent<TaskBubble>().taskIndex);
                GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", "[Task Bubble Option] Start");

                Debug.Log("Task Paused!");
            }
            else
            {
                GetComponentInParent<TaskBubble>().SetEdgeColor(true);
                AgentManager.instance.GetRobotAgent().StartTask(GetComponentInParent<TaskBubble>().taskIndex);
                GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", "[Task Bubble Option] Pause");

                Debug.Log("Task Started!");
            }


            GetComponentInParent<TaskBubble>().ResetExpandable();
            EventManager.instance.taskBubbleEvents.Selected(false);


        }
    }

}
