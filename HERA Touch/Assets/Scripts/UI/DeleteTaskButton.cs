using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class DeleteTaskButton : MonoBehaviour
{
    public void OnClicked()
    {
        AgentManager.instance.robotAgents[AgentManager.instance.currentAgent].DeleteTask(GetComponentInParent<TaskBubble>().index);
        EventManager.instance.taskBubbleEvents.Selected(false);
        Debug.Log("Task Deleted!");
    }

}
