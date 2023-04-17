using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    [Serializable]
    public struct PatientData
    {
        public string name;
        public int index;
        public Vector3 location;
    }

    [CreateAssetMenu(menuName = "HERA Touch/Patient List")]
    public class PatientList : ScriptableObject
    {
        public List<PatientData> patients;
    }

}
