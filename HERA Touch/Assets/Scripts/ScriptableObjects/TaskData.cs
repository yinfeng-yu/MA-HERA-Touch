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

        public string taskButtonLocalRefKey;
        public string tasksListEntryLocalRefKey;

        public Item requiredItem; // A Glass of water for giving water to patient

        public List<SiteEnum> availableTargetSiteEnums;

        public List<string> GetAvailableTargetsStringList()
        {
            List<string> sl = new List<string>();
            foreach (var se in availableTargetSiteEnums)
            {
                sl.Add(se.ToString());
            }
            return sl;
        }
    }

    
}
