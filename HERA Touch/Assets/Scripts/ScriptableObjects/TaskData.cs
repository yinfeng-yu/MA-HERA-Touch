using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    [CreateAssetMenu(menuName = "HERA Touch/Task Data")]
    public class TaskData : ScriptableObject
    {
        public new string name;
        public string description;

        public string buttonLocalRefKey;
        public string bubbleLocalRefKey;

        public List<Subtask> subtasks;


        public Item requiredItem; // A Glass of water for giving water to patient
    }

    
}
