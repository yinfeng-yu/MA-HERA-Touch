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

        public TMP_Dropdown targetSiteEnumSelectDropdown;
        public Button assignButton;

        public TasksButtonsControl tasksButtonsControl;

        public SiteEnum m_selectedTargetSiteEnum;

        protected override void Start()
        {
            base.Start();

            EventManager.instance.taskBubbleEvents.clicked += ResetExpandable;

            assignButton.onClick.AddListener(() => { AssignTask(); });

            var targetsList = taskData.GetAvailableTargetsStringList();

            targetSiteEnumSelectDropdown.ClearOptions();
            targetSiteEnumSelectDropdown.AddOptions(targetsList);

        }

        protected override void OnClicked()
        {
            base.OnClicked();
            EventManager.instance.taskButtonEvents.Clicked();
            EventManager.instance.taskButtonEvents.Selected(_selected);

            if (_selected) tasksButtonsControl.LerpTo(-GetComponent<RectTransform>().anchoredPosition.y);
        }

        public void SetTargetSiteEnum(int optionIndex)
        {
            m_selectedTargetSiteEnum = taskData.availableTargetSiteEnums[optionIndex];
        }

        void AssignTask()
        {
            // Debug.Log("Task Added!");

            AgentManager.instance.GetRobotAgent().AddTask(new Task(taskData, m_selectedTargetSiteEnum));

            GetComponent<Animator>().SetBool("selected", false);
            EventManager.instance.taskButtonEvents.Selected(false);
        }

    }

}
