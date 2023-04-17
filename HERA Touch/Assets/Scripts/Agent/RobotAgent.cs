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
        public int id;
        public Vector3 position;


        // public AgentState state;

        public event Action<Task, bool> _tasksChanged;
        public event Action _tasksSwapped;

        private List<Task> _tasks;
        private NavMeshAgent _agent;

        public float speed = 2f;


        // Start is called before the first frame update
        void Start()
        {
            _tasksChanged += OnTasksChanged;
            _tasksSwapped += OnTasksSwapped;
            _tasks = GetComponent<AgentTaskModule>().tasks;
            _agent = GetComponent<NavMeshAgent>();

            position = transform.position;
        }

        private void OnDestroy()
        {
            _tasksChanged -= OnTasksChanged;
            _tasksSwapped -= OnTasksSwapped;
        }

        private void Update()
        {
            // SetState();

            _agent.speed = speed;
        }

        // void SetState()
        // {
        //     if (IsStandingBy())
        //     {
        //         state = AgentState.StandingBy;
        //         stateLabel.GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", "[State] Standing by");
        //     }
        //     else
        //     {
        //         state = AgentState.TaskInProgress;
        //         stateLabel.GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", "[State] Task in Progress");
        //     }
        // }

        bool IsStandingBy()
        {
            if (_tasks.Count == 0) return true;
            foreach (var task in _tasks)
            {
                if (task.taskStatus == TaskStatus.OnGoing) return false;
            }
            return true;
        }

        void SetFirstTask()
        {
            if (_tasks.Count <= 1) return; // Nothing to manage.
            if (_tasks[0].taskStatus == TaskStatus.Paused)
            {
                MoveUpTask(1);
                _tasks[0].taskStatus = TaskStatus.OnGoing;
            }
        }

        public void MoveUpTask(int taskIndex)
        {
            if (taskIndex == 0) return;
            Task prevTask = _tasks[taskIndex - 1];
            _tasks[taskIndex - 1] = _tasks[taskIndex];
            _tasks[taskIndex] = prevTask;
            _tasksSwapped?.Invoke();
        }

        public void PriotizeTask(int taskIndex)
        {
            if (taskIndex == 0) return;
            while (taskIndex > 0)
            {
                MoveUpTask(taskIndex);
                taskIndex--;
            }
        }

        public void MoveDownTask(int taskIndex)
        {
            if (taskIndex == _tasks.Count - 1) return;
            Task nextTask = _tasks[taskIndex + 1];
            _tasks[taskIndex + 1] = _tasks[taskIndex];
            _tasks[taskIndex] = nextTask;
            _tasksSwapped?.Invoke();
        }

        public TaskStatus GetTaskStatus(int index)
        {
            return _tasks[index].taskStatus;
        }

        public void PauseTask(int taskIndex)
        {
            _tasks[taskIndex].taskStatus = TaskStatus.Paused;
        }

        public void StartTask(int taskIndex)
        {
            _tasks[0].taskStatus = TaskStatus.Paused;
            _tasks[taskIndex].taskStatus = TaskStatus.OnGoing;

            PriotizeTask(taskIndex);
        }

        public void AddTask(Task task)
        {
            if (_tasks.Count == 0)
            {
                task.taskStatus = TaskStatus.OnGoing;
            }
            _tasks.Add(task);
            _tasksChanged?.Invoke(task, true);
        }

        public void DeleteTask(int taskIndex)
        {
            Task task = _tasks[taskIndex];

            _tasks.Remove(task);
            _tasksChanged?.Invoke(task, false);

            EventManager.instance.taskBubbleEvents.Selected(false);
        }

        public void OnTasksChanged(Task task, bool add)
        {
            TasksPageManager.instance.taskDisplay.UpdateAgentTask(id, _tasks);
        }

        void OnTasksSwapped()
        {
            TasksPageManager.instance.taskDisplay.UpdateAgentTask(id, _tasks);
        }
    }

}
