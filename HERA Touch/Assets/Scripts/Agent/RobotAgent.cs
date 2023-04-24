using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.AI;

namespace HERATouch
{
    public enum AgentState
    {
        StandingBy,
        TaskInProgress,
    }

    [RequireComponent(typeof(AgentTaskModule))]
    [RequireComponent(typeof(AgentNavModule))]
    public class RobotAgent : MonoBehaviour
    {
        public Vector3 position;

        private List<Task> m_tasks;
        private NavMeshAgent m_navAgent;

        public float speed = 2f;

        public AgentTaskModule taskModule;

        // Start is called before the first frame update
        void Start()
        {
            taskModule = GetComponent<AgentTaskModule>();
            m_tasks = GetComponent<AgentTaskModule>().tasks;
            m_navAgent = GetComponent<NavMeshAgent>();

            position = transform.position;
            m_tasks = new List<Task>();
        }


        private void Update()
        {
            position = transform.position;
            m_navAgent.speed = speed;
        }

       
        public TaskStatus GetTaskStatus(int index)
        {
            return GetComponent<AgentTaskModule>().GetTaskStatus(index);
        }


        public void AddTask(Task task)
        {
            GetComponent<AgentTaskModule>().AddTask(task);
        }

        public void DeleteTask(int taskIndex)
        {
            GetComponent<AgentTaskModule>().DeleteTask(taskIndex);
        }

        public void PriotizeTask(int taskIndex)
        {
            GetComponent<AgentTaskModule>().PriotizeTask(taskIndex);
        }
        public void StartTask(int taskIndex)
        {
            GetComponent<AgentTaskModule>().StartTask(taskIndex);
        }

        public void PauseTask(int taskIndex)
        {
            m_tasks[taskIndex].taskStatus = TaskStatus.Paused;
        }

        public void CompleteTask(int taskIndex)
        {
            DeleteTask(taskIndex);
        }

    }

}
