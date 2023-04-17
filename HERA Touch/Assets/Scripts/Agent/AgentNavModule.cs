using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HERATouch
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentNavModule : MonoBehaviour
    {
        public Transform moveDestObject;
        private NavMeshAgent navMeshAgent;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            // navMeshAgent.destination = moveDestObject.position;
            Vector3 dest = GetComponent<AgentTaskModule>().GetCurrentDest();
            if (dest.y != 100)
            {
                navMeshAgent.destination = dest;

                if (GetComponent<AgentTaskModule>().tasks[0].taskStatus != TaskStatus.OnGoing)
                {
                    navMeshAgent.destination = transform.position;
                }
            }
            
        }
    }
}


