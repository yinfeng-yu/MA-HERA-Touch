using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HERATouch
{
    public class TasksPageManager : MonoBehaviour
    {
        #region Singleton
        public static TasksPageManager instance;
        private void Awake()
        {
            if (instance != this)
            {
                instance = this;
            }
        }
        #endregion

        public Transform content;
        public GameObject tasksListEntry;

        public ToggleMenu subpageToggleMenu;
        public GameObject newTaskNotificationDot;

        public GameObject noTasksText;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (!AgentManager.instance.GetRobotAgent().taskModule.HasTask())
            {
                newTaskNotificationDot.SetActive(false);
                noTasksText.SetActive(true);
            }
            else
            {
                newTaskNotificationDot.SetActive(true);
                noTasksText.SetActive(false);
                newTaskNotificationDot.GetComponentInChildren<TextMeshProUGUI>().text = AgentManager.instance.GetRobotAgent().taskModule.tasks.Count.ToString();
            }
        }

        public void UpdateTasksList(List<Task> tasks)
        {
            foreach (Transform child in content.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < tasks.Count; i++)
            {
                var go = Instantiate(tasksListEntry, content);


                go.GetComponent<TasksListEntry>().Initialize(i, tasks[i]);
            }

        }



    }
}

