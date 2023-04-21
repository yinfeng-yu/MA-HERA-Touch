using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public enum TaskType
    {
        BringWater,
        MeasureTemperature,
        MoveToSite,
    }

    [CreateAssetMenu(menuName = "HERA Touch/Task Data")]
    public class TaskData : ScriptableObject
    {
        public string description;
        public TaskType type;

        public string buttonLocalRefKey;
        public string bubbleLocalRefKey;

        public Item requiredItem; // A Glass of water for giving water to patient

        public List<Site> availableSites;

        [SerializeField] public List<Subtask> subtasks;
    }

    
}
