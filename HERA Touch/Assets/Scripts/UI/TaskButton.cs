using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HERATouch
{
    public class TaskButton : Expandable
    {
        public TaskData taskData;

        public TMP_Dropdown patientSelectDropdown;
        public Button assignButton;

        public TasksButtonsControl tasksButtonsControl;

        private int _selectedPatientId;

        protected override void Start()
        {
            base.Start();

            EventManager.instance.taskBubbleEvents.clicked += ResetExpandable;

            assignButton.onClick.AddListener(() => { AssignTask(); });

            var patientNamesList = PatientsManager.instance.GetPatientNames();

            patientSelectDropdown.ClearOptions();
            patientSelectDropdown.AddOptions(patientNamesList);

        }

        protected override void OnClicked()
        {
            base.OnClicked();
            EventManager.instance.taskButtonEvents.Clicked();
            EventManager.instance.taskButtonEvents.Selected(_selected);

            if (_selected) tasksButtonsControl.LerpTo(-GetComponent<RectTransform>().anchoredPosition.y);
            // if (GetComponent<Animator>().GetBool("inspected")) GetComponent<Animator>().SetBool("inspected", false);
        }

        private void Update()
        {
            
        }

        public void SetPatientId(int patientId)
        {
            _selectedPatientId = patientId;
        }

        void AssignTask()
        {
            Debug.Log("Task Added!");

            AgentManager.instance.GetRobotAgent().AddTask(new Task(taskData, _selectedPatientId));

            // GetComponent<Animator>().SetBool("inspected", false);
            GetComponent<Animator>().SetBool("selected", false);
            EventManager.instance.taskButtonEvents.Selected(false);
        }

    }

}
