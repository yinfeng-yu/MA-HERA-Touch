using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HERATouch
{
    public abstract class AgentBaseState
    {
        public string locRefKey;
        public abstract void EnterState(AgentStateModule agentStateModule);
        public abstract void UpdateState(AgentStateModule agentStateModule);
        public abstract void ExitState(AgentStateModule agentStateModule);
    }

}
