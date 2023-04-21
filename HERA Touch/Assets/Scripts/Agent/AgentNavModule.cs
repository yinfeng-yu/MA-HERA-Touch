using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HERATouch
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AgentNavModule : MonoBehaviour
    {
        private NavMeshAgent navMeshAgent;

        public Site destSite;
        public Vector3 destLocation;

        // public Site destinationSite;
        public LocationsList locationsList;

        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            destLocation = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (navMeshAgent.destination != destLocation)
            {
                navMeshAgent.destination = destLocation;
            }
        }

        public void SetDestination(Site _destSite)
        {
            destSite = _destSite;
            destLocation = locationsList.GetLocation(destSite);
        }

        // public void SetDestination(Site _destSite)
        // {
        //     foreach (var sl in locationsList.sitesLocations)
        //     {
        //         if (sl.site == _destSite)
        //         {
        //             navMeshAgent.destination = sl.location;
        //             return;
        //         }
        //     }
        // }
        // 
        // public void SetDestination(int patientId)
        // {
        //     destSite = locationsList.GetPatientSite(patientId);
        // }
    }
}


