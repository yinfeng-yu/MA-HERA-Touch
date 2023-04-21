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
            AgentNavModule agentNavModule = agentStateModule.gameObject.GetComponent<AgentNavModule>();

            if (agentTaskModule.HasTask())
            {
                
                if (agentTaskModule.HasCorrectItem())
                {
                    // Skip collecting, head to patient.
                    agentNavModule.SetDestination(agentTaskModule.GetCurrentTask().patientSite);
                    agentStateModule.SwitchState(agentStateModule.displacingState);

                }

                else if (agentTaskModule.HasItem())
                {
                    // Have the wrong item. Need to return first.
                    agentNavModule.SetDestination(agentTaskModule.GetCurrentItem().returnSite);
                    agentStateModule.SwitchState(agentStateModule.displacingState);
                }

                else
                {
                    agentNavModule.SetDestination(agentTaskModule.GetCurrentTask().taskData.requiredItem.collectSite);
                    // Empty hands. Need to move to collect location and then collect.
                    agentStateModule.SwitchState(agentStateModule.displacingState);
                }
            }

    }

        public override void ExitState(AgentStateModule agentStateModule)
        {

        }
    }
}

