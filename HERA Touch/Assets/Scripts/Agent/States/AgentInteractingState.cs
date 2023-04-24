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

            withPatient = SitesManager.instance.GetSite(agentNavModule.destSiteEnum).isPatient;
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
                    if (!withPatient) // Collecting/Returning done.
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
                    else // Interacting with patient done.
                    {
                        if (agentTaskModule.GetCurrentItem().shouldBeReturned)
                        {
                            if (agentTaskModule.GetNextTask() != null)
                            {
                                // We don't have to return the item. Just keep using it for the next task.
                                if (agentTaskModule.GetNextTask().taskData.requiredItem == agentTaskModule.GetCurrentItem())
                                {
                                    agentTaskModule.CompleteCurrentTask();
                                    agentStateModule.SwitchState(agentStateModule.idleState);
                                    return;
                                }
                            }
                            
                            agentNavModule.SetDestination(agentTaskModule.GetCurrentItem().returnSiteEnum);

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
