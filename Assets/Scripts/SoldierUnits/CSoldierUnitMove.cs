using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CSoldierUnitMove : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject UnitMeshObj;
    private GameObject DestinationHexagon;
    private bool IsReachedDestination = false;
    private bool IsMovingWithPlayerOrder = false;
    private bool IsMoving = false;  
    private float TimerForStopMovement = 0;
    private float MaxTimerForStopMovement = 1f;
    private const float rotSpeed = 200000f;
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity != Vector3.zero)
        {
            IsMoving = true;
            InstantlyTurn(agent.destination);
            if (IsMovingWithPlayerOrder)
            {
                agent.stoppingDistance = 0.1f;
                Vector3 direction = new Vector3(0, -0.1f, 0);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit))
                {
                    if (hit.collider.gameObject == DestinationHexagon && !IsReachedDestination)
                    {
                        IsReachedDestination = true;
                    }
                }
                if (IsReachedDestination)
                {
                    TimerForStopMovement += Time.deltaTime;
                    if (TimerForStopMovement > MaxTimerForStopMovement)
                    {
                        IsReachedDestination = false;
                        agent.isStopped = true;
                        TimerForStopMovement = 0;
                        IsMovingWithPlayerOrder = false;
                        IsMoving = false;
                    }
                }
            }
            else
            {
                agent.stoppingDistance = 0;
            }
            
        }
        
    }
    public void SetDestinationHexagon(GameObject dest_hexagon)
    {
        IsMovingWithPlayerOrder = true;
        IsReachedDestination = false;
        agent.isStopped = false;
        TimerForStopMovement = 0;
        DestinationHexagon = dest_hexagon;
       
    }
    public void SetDestinationEnemy()
    {
        IsReachedDestination = false;
        agent.isStopped = false;
        TimerForStopMovement = 0;
        
    }
    public void AgentInCombat(Vector3 destination)
    {
        InstantlyTurn(destination);
        agent.isStopped = true;
        
    }
    public void EnemyAgentDetected(GameObject enemy_obj)
    {
        if (!IsMovingWithPlayerOrder && !IsMoving)
        {
            SetDestinationEnemy();
            agent.destination = enemy_obj.transform.position;
        }
    }
    public void InstantlyTurn(Vector3 destination)
    {
        //When on target -> dont rotate!
    
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * rotSpeed);
    }
}
