using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton : Expandable
{
    public TaskData taskData;

    public Button assignButton;

    protected override void Start()
    {
        base.Start();

        EventManager.instance.taskBubbleEvents.clicked += ResetExpandable;
        SwipeManager.instance.swipeUp += OnSwipeUp;

        assignButton.onClick.AddListener(() => { AssignTask(); });
    }

    protected override void OnClicked()
    {
        base.OnClicked();
        EventManager.instance.taskButtonEvents.Clicked();
        EventManager.instance.taskButtonEvents.Selected(_selected);

        if (GetComponent<Animator>().GetBool("inspected")) GetComponent<Animator>().SetBool("inspected", false);
    }

    void OnSwipeUp()
    {
        if (_selected) 
        {
            // Debug.Log("Task Added!");
            // AgentManager.instance.robotAgents[AgentManager.instance.currentAgent].AddTask(new Task(taskData));

            // ResetExpandable();

            Debug.Log("Task Inspected");
            GetComponent<Animator>().SetBool("inspected", true);

        }
    }

    void AssignTask()
    {
        Debug.Log("Task Added!");
        AgentManager.instance.robotAgents[AgentManager.instance.currentAgent].AddTask(new Task(taskData));

        GetComponent<Animator>().SetBool("inspected", false);
        GetComponent<Animator>().SetBool("selected", false);
    }

}
