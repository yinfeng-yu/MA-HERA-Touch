using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public class AgentIdleState : AgentBaseState
    {
        // public new string locRefKey = "[State] Standing by";

        public AgentIdleState()
        {
            locRefKey = "[State] Standing by";
        }

        public override void EnterState(AgentStateModule agentStateModule)
        {
            Debug.Log("Start idling.");
        }

        public override void UpdateState(AgentStateModule agentStateModule)
        {
            AgentTaskModule agentTaskModule = agentStateModule.gameObject.GetComponent<AgentTaskModule>();
            if (agentTaskModule.HasTask())
            {
                
                if (agentTaskModule.HasCorrectItem())
                {
                    // Skip collecting, head to patient.
                    agentStateModule.SwitchState(agentStateModule.displacingState);

                }

                else if (agentTaskModule.HasItem())
                {
                    // Have the wrong item. Need to return first.
                    agentStateModule.SwitchState(agentStateModule.returnState);
                }

                else
                {
                    // Empty hands. Need to collect.
                    agentStateModule.SwitchState(agentStateModule.collectingState);
                }
            }

    }

        public override void ExitState(AgentStateModule agentStateModule)
        {

        }
    }
}

