using System;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public enum TaskStatus
    {
        Paused,
        OnGoing,
        Completed,
    }

    public enum SubtaskType
    {
        CollectingObject,
        ReturningObject,
        InteractingWithPatient,
    }

    [Serializable]
    public class Subtask
    {
        public string name;
        public SubtaskType type;

        public Vector3 location;
        public float requiredTime;
        public float usedTime = 0;
        public float progress = 0;

        public Subtask(string _name, Vector3 _location, float _requiredTime)
        {
            name = _name;
            location = _location;
            requiredTime = _requiredTime;
        }
    }

    [Serializable]
    public class Task
    {
        public TaskData taskData;
        public TaskStatus taskStatus = TaskStatus.Paused;
        
        public List<Subtask> subtasks;

        public int patientId = -1;

        public Task(TaskData _taskData, int _patientId, List<Subtask> _subtasks)
        {
            taskData = _taskData;
            patientId = _patientId;
            subtasks = _subtasks;
        }

    }
}

