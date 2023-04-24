using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public class AgentDisplacingState : AgentBaseState
    {
        private Vector3 destLocation;


        public AgentDisplacingState()
        {
            locRefKey = "[State] Task in Progress";
        }


        public override void EnterState(AgentStateModule agentStateModule)
        {
            Debug.Log("Start moving.");
            AgentNavModule agentNavModule = agentStateModule.gameObject.GetComponent<AgentNavModule>();
            destLocation = agentNavModule.destLocation;
        }

        public override void UpdateState(AgentStateModule agentStateModule)
        {
            Transform agentTransform = agentStateModule.gameObject.transform;

            // Has arrived at the destLocation.
            if (Vector3.Distance(agentTransform.position, destLocation) <= 0.1f)
            {
                AgentTaskModule agentTaskModule = agentStateModule.gameObject.GetComponent<AgentTaskModule>();

                agentTransform.position = destLocation;

                if (agentTaskModule.GetCurrentItem().type == agentTaskModule.GetCurrentTask().taskData.requiredItem.type &&
                    agentTaskModule.GetCurrentItem().type == ItemType.None)
                {
                    agentTaskModule.CompleteCurrentTask();
                    agentStateModule.SwitchState(agentStateModule.idleState);
                }
                else
                {
                    agentStateModule.SwitchState(agentStateModule.interactingState);
                }
                
            }
        }

        public override void ExitState(AgentStateModule agentStateModule)
        {

        }
    }
}

