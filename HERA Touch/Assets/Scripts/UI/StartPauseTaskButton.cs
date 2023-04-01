using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public class StartPauseTaskButton : MonoBehaviour
{
    private void Start()
    {
        int index = GetComponentInParent<TaskBubble>().index;
        if (AgentManager.instance.robotAgents[AgentManager.instance.currentAgent].tasks[index].taskState == TaskState.OnGoing)
        {
            GetComponentInParent<TaskBubble>().SetEdgeColor(true);
            GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("TouchMate Table", "[Task Bubble Option] Pause");
        }
        else
        {
            GetComponentInParent<TaskBubble>().SetEdgeColor(false);
            GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("TouchMate Table", "[Task Bubble Option] Start");
        }
    }

    public void OnClicked()
    {
        int index = GetComponentInParent<TaskBubble>().index;
        if (AgentManager.instance.robotAgents[AgentManager.instance.currentAgent].tasks[index].taskState == TaskState.OnGoing)
        {
            GetComponentInParent<TaskBubble>().SetEdgeColor(false);
            AgentManager.instance.robotAgents[AgentManager.instance.currentAgent].PauseTask(GetComponentInParent<TaskBubble>().index);
            GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("TouchMate Table", "[Task Bubble Option] Start");

            Debug.Log("Task Paused!");
        }
        else
        {
            GetComponentInParent<TaskBubble>().SetEdgeColor(true);
            AgentManager.instance.robotAgents[AgentManager.instance.currentAgent].StartTask(GetComponentInParent<TaskBubble>().index);
            GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("TouchMate Table", "[Task Bubble Option] Pause");

            Debug.Log("Task Started!");
        }
        

        GetComponentInParent<TaskBubble>().ResetExpandable();
        EventManager.instance.taskBubbleEvents.Selected(false);

        
    }
}
