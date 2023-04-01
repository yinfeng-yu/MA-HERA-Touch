using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public enum AgentState
{
    StandingBy,
    TaskInProgress,
}

public class RobotAgent : MonoBehaviour
{
    public int id;
    public Vector3 position;

    public List<Task> tasks;

    public AgentState state;
    public GameObject stateLabel;

    public event Action<Task, bool> tasksChanged;
    public event Action tasksSwapped;

    // Start is called before the first frame update
    void Start()
    {
        tasksChanged += OnTasksChanged;
        tasksSwapped += OnTasksSwapped;

    }

    private void OnDestroy()
    {
        tasksChanged -= OnTasksChanged;
    }

    private void Update()
    {
        SetState();
        // SetFirstTask();
    }

    void SetState()
    {
        if (IsStandingBy())
        {
            state = AgentState.StandingBy;
            stateLabel.GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("TouchMate Table", "[State] Standing by");
        }
        else
        {
            state = AgentState.TaskInProgress;
            stateLabel.GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("TouchMate Table", "[State] Task in Progress");
        }
    }

    bool IsStandingBy()
    {
        if (tasks.Count == 0) return true;
        foreach (var task in tasks)
        {
            if (task.taskState == TaskState.OnGoing) return false;
        }
        return true;
    }

    void SetFirstTask()
    {
        if (tasks.Count <= 1) return; // Nothing to manage.
        if (tasks[0].taskState == TaskState.Paused)
        {
            MoveUpTask(1);
            tasks[0].taskState = TaskState.OnGoing;
        }
    }

    public void MoveUpTask(int taskIndex)
    {
        if (taskIndex == 0) return;
        Task prevTask = tasks[taskIndex - 1];
        tasks[taskIndex - 1] = tasks[taskIndex];
        tasks[taskIndex] = prevTask;
        tasksSwapped?.Invoke();
    }

    public void PriotizeTask(int taskIndex)
    {
        if (taskIndex == 0) return;
        while (taskIndex > 0)
        {
            MoveUpTask(taskIndex);
            taskIndex --;
        }
    }

    public void MoveDownTask(int taskIndex)
    {
        if (taskIndex == tasks.Count - 1) return;
        Task nextTask = tasks[taskIndex + 1];
        tasks[taskIndex + 1] = tasks[taskIndex];
        tasks[taskIndex] = nextTask;
        tasksSwapped?.Invoke();
    }

    public void PauseTask(int taskIndex)
    {
        tasks[taskIndex].taskState = TaskState.Paused;
    }

    public void StartTask(int taskIndex)
    {
        tasks[0].taskState = TaskState.Paused;
        tasks[taskIndex].taskState = TaskState.OnGoing;

        PriotizeTask(taskIndex);
    }

    public void AddTask(Task task)
    {
        task.index = tasks.Count;
        if (task.index == 0)
        {
            task.taskState = TaskState.OnGoing;
        }
        tasks.Add(task);
        tasksChanged?.Invoke(task, true);
    }

    public void DeleteTask(int taskIndex)
    {
        Task task = tasks[taskIndex];

        tasks.Remove(task);
        tasksChanged?.Invoke(task, false);
    }

    public void OnTasksChanged(Task task, bool add)
    {
        TasksPageManager.instance.taskDisplay.UpdateAgentTask(id, tasks);
    }

    void OnTasksSwapped()
    {
        TasksPageManager.instance.taskDisplay.UpdateAgentTask(id, tasks);
    }
}
