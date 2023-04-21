using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public class AgentInteractingState : AgentBaseState
    {
        bool withPatient = false;

        private float _requiredTime = 2f;
        private float _usedTime = 0f;

        public AgentInteractingState()
        {
            locRefKey = "[State] Task in Progress";
        }

        public override void EnterState(AgentStateModule agentStateModule)
        {
            Debug.Log("Start interacting.");
            AgentNavModule agentNavModule = agentStateModule.gameObject.GetComponent<AgentNavModule>();
            withPatient = agentNavModule.locationsList.IsPatientWard(agentNavModule.destSite);
        }

        public override void UpdateState(AgentStateModule agentStateModule)
        {
            AgentTaskModule agentTaskModule = agentStateModule.gameObject.GetComponent<AgentTaskModule>();
            AgentNavModule agentNavModule = agentStateModule.gameObject.GetComponent<AgentNavModule>();

            if (agentTaskModule.HasTask())
            {
                if (_usedTime >= _requiredTime)
                {
                    // Interaction completed.
                    if (!withPatient)
                    {
                        // Was collecting item.
                        if (agentTaskModule.GetCurrentItem().type == ItemType.None)
                        {
                            agentTaskModule.SetCurrentItem(agentTaskModule.GetCurrentTask().taskData.requiredItem);
                            agentStateModule.SwitchState(agentStateModule.idleState);
                        }
                        // Was returning item.
                        else
                        {
                            // Empty the hands.
                            agentTaskModule.SetCurrentItem(agentTaskModule.emptyItem);
                            // Finish the current task.
                            agentTaskModule.CompleteCurrentTask();
                            // Idle.
                            agentStateModule.SwitchState(agentStateModule.idleState);
                        }
                    }
                    else
                    {
                        if (agentTaskModule.GetCurrentItem().shouldBeReturned)
                        {
                            agentNavModule.SetDestination(agentTaskModule.GetCurrentItem().returnSite);
                            agentStateModule.SwitchState(agentStateModule.displacingState);
                        }
                        else
                        {
                            agentTaskModule.SetCurrentItem(agentTaskModule.emptyItem);
                            agentTaskModule.CompleteCurrentTask();
                            agentStateModule.SwitchState(agentStateModule.idleState);
                        }
                    }
                }
                else
                {
                    _usedTime += Time.deltaTime;
                }
            }
            else
            {
                agentStateModule.SwitchState(agentStateModule.idleState);
            }

            
        }

        public override void ExitState(AgentStateModule agentStateModule)
        {
            _usedTime = 0f;
        }
    }

}
