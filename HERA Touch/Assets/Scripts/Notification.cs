using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HERATouch
{
    public class Notification
    {
        public TaskType taskType;
        public string patientName;
        public string data;
        public string localRefKey;
        public int timeStamp;

        public Notification(TaskType a_taskType, string a_patientName, string a_data, string a_localRefKey, int a_timeStamp = 0)
        {
            taskType = a_taskType;
            patientName = a_patientName;
            data = a_data;
            localRefKey = a_localRefKey;
            timeStamp = a_timeStamp;
        }
    }

    

}
