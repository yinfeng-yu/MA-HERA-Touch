using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // For Test use: To add the robots to the manager
    public List<RobotAgent> robotAgents;
    public int currentAgent = 0;

    public GameObject idLabel;

    private List<RobotAgent> _robotAgents;

    // Start is called before the first frame update
    void Start()
    {
        if (robotAgents.Count == 0)
        {
            Debug.LogError("No Robot agent detected!");
        }
        else
        {
            _robotAgents = new List<RobotAgent>();
            foreach (var agent in robotAgents)
            {
                AddAgent(agent);
            }
        }

        idLabel.GetComponentInChildren<TextMeshProUGUI>().text = "ID: " + currentAgent.ToString();
    }


    public void AddAgent(RobotAgent agent)
    {
        agent.id = _robotAgents.Count;
        _robotAgents.Add(agent);
    }

}
