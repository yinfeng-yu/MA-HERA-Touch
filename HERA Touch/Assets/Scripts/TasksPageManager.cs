using System;
using UnityEngine;

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

    public TasksListDisplay taskDisplay;



    // Start is called before the first frame update
    void Start()
    {
        taskDisplay = GetComponentInChildren<TasksListDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
