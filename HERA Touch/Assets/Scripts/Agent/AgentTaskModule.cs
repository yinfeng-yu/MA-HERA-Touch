using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public enum TaskExeMode
    {
        ExeNextWhenComplete, // Default Mode
        PauseNextWhenComplete,
    }

    public class AgentTaskModule : MonoBehaviour
    {
        private RobotAgent _robotAgent;
        // private TaskExeMode _taskExeMode = TaskExeMode.ExeNextWhenComplete;

        public List<Task> tasks;

        public Item emptyItem;
        [SerializeField] private Item _itemOnHand;
        // public ItemLocations itemLocations;

        private Vector3 _curDest;

        public bool subtaskCompleted = true;

        // [SerializeField] private float _requiredTime = 2f;
        // [SerializeField] private float _usedTime = 0f;

        // Start is called before the first frame update
        void Start()
        {
            _robotAgent = GetComponent<RobotAgent>();
            _itemOnHand = emptyItem;

            _curDest = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void AddTask(Task task)
        {
            if (tasks.Count == 0)
            {
                task.taskStatus = TaskStatus.OnGoing;
            }
            tasks.Add(task);
            TasksPageManager.instance.UpdateTasksList(tasks);
        }

        public void DeleteTask(int taskIndex)
        {
            tasks.RemoveAt(taskIndex);
            TasksPageManager.instance.UpdateTasksList(tasks);
        }

        public void PriotizeTask(int taskIndex)
        {
            if (taskIndex == 0) return;
            Task task = tasks[taskIndex];

            for (int i = taskIndex; i > 0; i--)
            {
                tasks[i] = tasks[i - 1];
            }
            tasks[0] = task;
        }

        public void StartTask(int taskIndex)
        {
            tasks[0].taskStatus = TaskStatus.Paused;
            tasks[taskIndex].taskStatus = TaskStatus.OnGoing;

            PriotizeTask(taskIndex);
        }

        public TaskStatus GetTaskStatus(int index)
        {
            if (index < tasks.Count) return tasks[index].taskStatus;
            return TaskStatus.Paused;
        }

        public bool HasTask()
        {
            return tasks.Count != 0;
        }

        public bool HasTaskInProgress()
        {
            if (!HasTask()) return false;
            return tasks[0].taskStatus == TaskStatus.OnGoing;
        }

        public Task GetCurrentTask()
        {
            return tasks[0];
        }

        public Task GetNextTask()
        {
            if (tasks.Count >= 2)
            {
                return tasks[1];
            }
            return null;
        }

        public bool HasItem()
        {
            return _itemOnHand.type != ItemType.None;
        }

        public bool HasCorrectItem()
        {
            return _itemOnHand == tasks[0].taskData.requiredItem;
        }

        public void CollectItem(Item item)
        {
            _itemOnHand = item;
        }

        public void DropItem()
        {
            _itemOnHand.type = ItemType.None;
        }

        public Item GetCurrentItem()
        {
            return _itemOnHand;
        }

        public void SetCurrentItem(Item item)
        {
            _itemOnHand = item;
        }

        public void CompleteCurrentTask()
        {
            NotificationManager.instance.Notify(GetCurrentTask().GetTaskType(), GetCurrentTask().targetSiteEnum, "36.5");
            GetComponent<RobotAgent>().CompleteTask(0);
        }

        // public void StartSubtask(float requiredTime = 2f)
        // 
        // {
        //     Debug.Log("start subtask");
        //     subtaskCompleted = false;
        //     _usedTime = 0f;
        //     _requiredTime = requiredTime;
        // 
        // }

    }
}