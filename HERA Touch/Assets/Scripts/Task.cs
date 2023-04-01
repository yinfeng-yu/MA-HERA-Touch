using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskState
{
    Paused,
    OnGoing,
    Finished
}

[System.Serializable]
public class Task
{
    public TaskData taskData;
    public int index = 0;
    public TaskState taskState = TaskState.Paused;

    public Task(TaskData _taskData)
    {
        taskData = _taskData;
    }

}
