using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace HERATouch
{
    public class AgentStateModule : MonoBehaviour
    {
        public AgentBaseState currentState;

        // public AgentCollectingState collectingState = new AgentCollectingState();
        public AgentDisplacingState displacingState = new AgentDisplacingState();
        public AgentInteractingState interactingState = new AgentInteractingState();
        public AgentIdleState idleState = new AgentIdleState();
        // public AgentReturnState returnState = new AgentReturnState();
        // public AgentMoveToCollectLocState moveToCollectLocState = new AgentMoveToCollectLocState();
        // public AgentMoveToPatientState moveToPatientState = new AgentMoveToPatientState();

        

        private void Start()
        {
            currentState = idleState;
            currentState.EnterState(this);
        }

        private void Update()
        {
            currentState.UpdateState(this);

            // Update the agent state UI.
            UIManager.instance.stateLabel.GetComponentInChildren<LocalizeStringEvent>().StringReference.SetReference("HERA Touch Table", currentState.locRefKey);
        }

        public void SwitchState(AgentBaseState state)
        {
            currentState.ExitState(this);
            currentState = state;
            state.EnterState(this);
        }
    }

}
