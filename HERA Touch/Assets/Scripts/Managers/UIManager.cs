using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;
    private void Awake()
    {
        if (instance != this)
        {
            instance = this;
        }
    }

    #endregion

    public GameObject agentStateLabel;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     stateLabel = GameObject.Find("State Label");
    // }
    // 
    // // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}
