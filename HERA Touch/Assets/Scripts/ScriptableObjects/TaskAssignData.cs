using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    [CreateAssetMenu(menuName = "HERA Touch/Task Assign Data")]
    public class TaskAssignData : ScriptableObject
    {
        public TaskData data;
        public int patientId;

    }
}
