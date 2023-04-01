using System;
using UnityEngine.EventSystems;
using UnityEngine;

public class TaskButton : Expandable
{
    public TaskData taskData;

    protected override void Start()
    {
        base.Start();

        EventManager.instance.taskBubbleEvents.clicked += ResetExpandable;
        SwipeManager.instance.swipeUp += OnSwipeUp;
    }

    protected override void OnClicked()
    {
        base.OnClicked();
        EventManager.instance.taskButtonEvents.Clicked();
        EventManager.instance.taskButtonEvents.Selected(_selected);
    }

    void OnSwipeUp()
    {
        if (_selected) 
        {
            Debug.Log("Task Added!");
            AgentManager.instance.robotAgents[AgentManager.instance.currentAgent].AddTask(new Task(taskData));

            ResetExpandable();
        }
    }

}
