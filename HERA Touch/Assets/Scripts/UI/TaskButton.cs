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
        // public TaskAssignData taskAssignData;

        public TMP_Dropdown patientSelectDropdown;
        public Button assignButton;

        private int _selectedPatientId;

        protected override void Start()
        {
            base.Start();

            EventManager.instance.taskBubbleEvents.clicked += ResetExpandable;
            SwipeManager.instance.swipeUp += OnSwipeUp;

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

            if (GetComponent<Animator>().GetBool("inspected")) GetComponent<Animator>().SetBool("inspected", false);
        }

        void OnSwipeUp()
        {
            if (_selected)
            {

                // Debug.Log("Task Inspected");

                _selectedPatientId = patientSelectDropdown.value;

                GetComponent<Animator>().SetBool("inspected", true);

            }
        }

        public void SetPatientId(int patientId)
        {
            _selectedPatientId = patientId;
        }

        void AssignTask()
        {
            // Debug.Log("Task Added!");

            List<Subtask> subtasks = new List<Subtask>();
            for (int i = 0; i < taskData.subtasks.Count; i ++)
            {
                if (taskData.subtasks[i].type == SubtaskType.InteractingWithPatient)
                {
                    Subtask subtask = new Subtask(taskData.subtasks[i].name, PatientsManager.instance.patientList.patients[_selectedPatientId].location, taskData.subtasks[i].requiredTime);
                    subtasks.Add(subtask);
                }
                else 
                {
                    Subtask subtask = new Subtask(taskData.subtasks[i].name, taskData.subtasks[i].location, taskData.subtasks[i].requiredTime);
                    subtasks.Add(subtask);
                }
            }
            AgentManager.instance.GetRobotAgent().AddTask(new Task(taskData, _selectedPatientId, subtasks));

            GetComponent<Animator>().SetBool("inspected", false);
            GetComponent<Animator>().SetBool("selected", false);
            EventManager.instance.taskButtonEvents.Selected(false);
        }

    }

}
