using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class TasksListDisplay : MonoBehaviour
{
    public List<List<Task>> _agentsTasks;

    public Transform content;
    public GameObject taskDetail;

    // Start is called before the first frame update
    void Start()
    {
        _agentsTasks = new List<List<Task>>();
        _agentsTasks.Add(new List<Task>());

    }


    public void UpdateAgentTask(int id, List<Task> tasks)
    {
        Debug.Log("Update Task display");
        _agentsTasks[id] = tasks;

        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < _agentsTasks[id].Count; i ++)
        {
            var go = Instantiate(taskDetail, content);
            go.GetComponent<TaskBubble>().index = i;
            go.GetComponent<TaskBubble>().SetEdgeColor(tasks[i].taskState == TaskState.OnGoing);
            go.GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("TouchMate Table", tasks[i].taskData.localReferenceKey);

        }

        GetComponentInChildren<ScrollSnapRect>().UpdatePages(_agentsTasks[id].Count);
    }
}
