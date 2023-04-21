using System;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public enum SubtaskType
    {
        CollectingObject,
        ReturningObject,
        MovingToLocation,
        InteractingWithPatient,
    }

    

    [Serializable]
    public class Subtask
    {
        public SubtaskType type;

        [MyBox.ConditionalField(nameof(type), true, SubtaskType.MovingToLocation)]
        public float requiredTime = 2f;

        [MyBox.ConditionalField(nameof(type), false, SubtaskType.CollectingObject)]
        public Item item;

        [MyBox.ConditionalField(nameof(type), false, SubtaskType.MovingToLocation)]
        public Site site;

        [HideInInspector]
        public int patientId;

    }

}
