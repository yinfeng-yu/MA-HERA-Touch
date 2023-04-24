using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HERATouch
{
    public class AgentManager : MonoBehaviour
    {
        #region Singleton
        public static AgentManager instance;
        private void Awake()
        {
            if (instance != this)
            {
                instance = this;
            }
        }
        #endregion

        [SerializeField]
        private RobotAgent _robotAgent;

        public List<Item> itemsPool;

        //public GameObject idLabel;

        // Start is called before the first frame update
        void Start()
        {
            if (!_robotAgent) Debug.Log("No robot agent initialized!");

            //idLabel.GetComponentInChildren<TextMeshProUGUI>().text = "ID: " + currentAgent.ToString();
        }

        public RobotAgent GetRobotAgent()
        {
            return _robotAgent;
        }

    }

}
