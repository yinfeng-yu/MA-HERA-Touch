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

    

    // [Serializable]
    // public class Subtask
    // {
    //     public string name;
    //     public SubtaskType type;
    // 
    //     public Vector3 location;
    //     public float requiredTime;
    //     public float usedTime = 0;
    //     public float progress = 0;
    // 
    //     public Subtask(string _name, Vector3 _location, float _requiredTime)
    //     {
    //         name = _name;
    //         location = _location;
    //         requiredTime = _requiredTime;
    //     }
    // }

    [Serializable]
    public class Task
    {
        public TaskData taskData;
        public TaskStatus taskStatus = TaskStatus.Paused;

        public int patientId = -1;
        public Site patientSite;

        public Task(TaskData _taskData, int _patientId)
        {
            taskData = _taskData;
            patientId = _patientId;

            patientSite = PatientsManager.instance.patientsList.patients[_patientId].site;
        }

        public TaskType GetTaskType()
        {
            return taskData.type;
        }
    }
}

