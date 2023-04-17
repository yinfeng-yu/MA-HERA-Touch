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
        private TaskExeMode _taskExeMode = TaskExeMode.ExeNextWhenComplete;

        public List<Task> tasks;

        public Item itemOnHand;

        // Start is called before the first frame update
        void Start()
        {
            _robotAgent = GetComponent<RobotAgent>();
            itemOnHand = Item.None;
        }

        // Update is called once per frame
        void Update()
        {
            // if (tasks.Count > 0) // We have some tasks in the queue.
            // {
            //     Task curTask = tasks[0];
            // 
            //     if (curTask.taskStatus != TaskStatus.OnGoing) return;
            // 
            //     if (GetComponent<RobotAgent>().itemOnHand.type == curTask.requiredItem.type) // The item matches.
            //     {
            //         // Is the task done?
            //         if (curTask.taskStatus != TaskStatus.Completed) // Not done. Go to the patient.
            //         {
            //             if (Vector3.Distance(transform.position, curTask.subtasks[0].location) <= 0.1f) // Arrived.
            //             {
            //                 // Interact with patient.
            //                 
            //                 curTask.subtasks[0].usedTime += Time.deltaTime;
            //                 curTask.subtasks[0].progress = curTask.subtasks[0].usedTime / curTask.subtasks[0].requiredTime;
            //                 if (curTask.subtasks[0].usedTime >= curTask.subtasks[0].requiredTime)
            //                 {
            //                     // The current subtask is done;
            //                     curTask.subtasks.RemoveAt(0);
            //                 }
            //             }
            //             else // Not yet arrived.
            //             {
            // 
            //             }
            //         }
            // 
            // 
            // 
            //     }
            // 
            // 
            // 
            // }




            if (tasks.Count > 0)
            {
                Task curTask = tasks[0];

                if (curTask.taskStatus != TaskStatus.OnGoing)
                {
                    return;
                }

                if (curTask.subtasks.Count == 0)
                {
                    // The task is completed.

                    _robotAgent.DeleteTask(0);
                    
                    // Debug.Log("Task Finished!");
                    if (tasks.Count > 0 && _taskExeMode == TaskExeMode.ExeNextWhenComplete)
                    {
                        _robotAgent.StartTask(0);
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, curTask.subtasks[0].location) <= 0.1f)
                    {
                        curTask.subtasks[0].usedTime += Time.deltaTime;
                        curTask.subtasks[0].progress = curTask.subtasks[0].usedTime / curTask.subtasks[0].requiredTime;
                        if (curTask.subtasks[0].usedTime >= curTask.subtasks[0].requiredTime)
                        {
                            // The current subtask is done;
                            curTask.subtasks.RemoveAt(0);
                        }
                    }
                    
                }
                
            }
        }

        public bool HasTask()
        {
            return tasks.Count != 0;
        }

        public Task GetCurrentTask()
        {
            return tasks[0];
        }

        public bool HasItem()
        {
            return itemOnHand != Item.None;
        }

        public bool HasCorrectItem()
        {
            return itemOnHand == tasks[0].taskData.requiredItem;
        }

        public Vector3 GetCurrentDest()
        {
            if (tasks.Count > 0)
            {
                if (tasks[0].subtasks.Count > 0)
                {
                    return tasks[0].subtasks[0].location;
                }
            }
            return new Vector3(0, 100, 0); // 100 is detected as an invalid digit.
        }

        public float GetCurrentSubtaskProgress()
        {
            if (tasks.Count > 0)
            {
                if (tasks[0].subtasks.Count > 0)
                {
                    return tasks[0].subtasks[0].progress;
                }
            }
            return 0;
        }

    }
}