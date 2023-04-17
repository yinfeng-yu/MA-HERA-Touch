using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public class AgentCollectingState : AgentBaseState
    {
        public AgentCollectingState()
        {
            locRefKey = "[State] Task in Progress";
        }

        public override void EnterState(AgentStateModule agentStateModule)
        {
            Debug.Log("Start collecting.");
        }

        public override void UpdateState(AgentStateModule agentStateModule)
        {
            AgentTaskModule agentTaskModule = agentStateModule.gameObject.GetComponent<AgentTaskModule>();

            if (!agentTaskModule.HasTask())
            {
                // If no task in the queue, stand by.
                agentStateModule.SwitchState(agentStateModule.idleState);

            }
        }

        public override void ExitState(AgentStateModule agentStateModule)
        {

        }
    }

}
