using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HERATouch
{
    public class PatientsManager : MonoBehaviour
    {
        #region Singleton
        public static PatientsManager instance;
        private void Awake()
        {
            if (instance != this)
            {
                instance = this;
            }
        }
        #endregion

        public PatientList patientList;

        public List<string> GetPatientNames()
        {
            List<string> patientNames = new List<string>();
            for (int i = 0; i < patientList.patients.Count; i++)
            {
                patientNames.Add(patientList.patients[i].name);
            }
            return patientNames;
        }
    }

}
