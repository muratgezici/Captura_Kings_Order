using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CSoldierAgentMove : MonoBehaviour
{
    NavMeshAgent agent;
    private GameObject DestinationHexagon;
    private bool IsReachedDestination = false;
    private float TimerForStopMovement = 0;
    private float MaxTimerForStopMovement = 1f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity != Vector3.zero)
        {
            Vector3 direction = new Vector3(0, -0.1f, 0);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if(hit.collider.gameObject == DestinationHexagon && !IsReachedDestination)
                {
                    IsReachedDestination = true;
                }
            }
        }
        if(IsReachedDestination)
        {
            TimerForStopMovement += Time.deltaTime;
            if(TimerForStopMovement >  MaxTimerForStopMovement)
            {
                IsReachedDestination = false;
                agent.destination = transform.position;
                TimerForStopMovement = 0;
            }
        }
    }
    public void SetDestinationHexagon(GameObject dest_hexagon)
    {
        IsReachedDestination = false;
        TimerForStopMovement = 0;
        DestinationHexagon = dest_hexagon;
    }
}
