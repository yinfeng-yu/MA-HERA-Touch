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


    [Serializable]
    public class Task
    {
        public TaskData taskData;
        public TaskStatus taskStatus = TaskStatus.Paused;

        public int patientId = -1;
        public SiteEnum targetSiteEnum;

        public Task(TaskData a_taskData, int a_patientId)
        {
            taskData = a_taskData;
            patientId = a_patientId;

            targetSiteEnum = SitesManager.instance.GetPatientSiteEnum(a_patientId);
        }

        public Task(TaskData a_taskData, SiteEnum a_siteEnum)
        {
            taskData = a_taskData;

            targetSiteEnum = a_siteEnum;
        }

        public TaskType GetTaskType()
        {
            return taskData.type;
        }
    }
}

