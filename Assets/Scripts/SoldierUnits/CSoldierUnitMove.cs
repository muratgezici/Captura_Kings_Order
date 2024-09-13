using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CSoldierUnitMove : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject UnitMeshObj;
    [SerializeField] private CSoldierUnitCombat SoldierUnitCombat;
    [SerializeField] private CSoldierUnitDetection SoldierUnitDetection;
    [SerializeField] private float DestinationSpeed;
    [SerializeField] private float FollowSpeed;
    private GameObject DestinationObject;

    private bool IsReachedDestination = false;

    private bool IsMovingToHexagon = false;
    private bool IsMovingToDetectedEnemy = false;
    private bool IsMovingToCombatEnemy = false;

    private bool IsMoving = false;  

    private float TimerForStopMovement = 0;
    private float MaxTimerForStopMovement = 1f;
    private const float rotSpeed = 200000f;
    private float destinationReachedTreshold = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

   //Bu script sadece move ile ilgilenir. Hedefe ulaþýnca hedefe ulaþtýðýný bildirir.
   //InRange enemy, Combat enemy veya týklanan yere gitmeyi düzenler.
   public void ResetVariables()
    {
        DestinationObject = null;
        IsReachedDestination = false;
        IsMovingToHexagon = false;
        IsMovingToDetectedEnemy = false;
        IsMoving = false;
        TimerForStopMovement = 0;
        MaxTimerForStopMovement = 1f;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.isStopped = false;
        destinationReachedTreshold = agent.stoppingDistance;

    }
    void Update()
    {
        
        
    }
  
    public void SetMoveToClickedPosition(GameObject dest_hexagon)
    {
        ResetVariables();
        IsMovingToHexagon = true;
        DestinationObject = dest_hexagon;
        agent.stoppingDistance = 0.1f;
        destinationReachedTreshold = agent.stoppingDistance;
        agent.speed = DestinationSpeed;

    }
    public void SetMoveToNearestEnemyPosition(GameObject near_enemy)
    {
        ResetVariables();
        IsMovingToDetectedEnemy = true;
        DestinationObject = near_enemy;
        agent.stoppingDistance = 0.01f;
        destinationReachedTreshold = agent.stoppingDistance;
        agent.speed = FollowSpeed;
    }
    public bool MoveToClickedPos()
    {
        if (agent.velocity != Vector3.zero)
        {
            IsMoving = true;
            InstantlyTurn(agent.destination);
            if (IsMovingToHexagon)
            {
                Vector3 direction = new Vector3(0, -0.1f, 0);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit))
                {
                    if (hit.collider.gameObject == DestinationObject && !IsReachedDestination)
                    {
                        IsReachedDestination = true;
                    }
                }
                float distanceToTarget = Vector3.Distance(transform.position, agent.destination);
                if (distanceToTarget < destinationReachedTreshold)
                {
                    IsReachedDestination = false;
                    agent.isStopped = true;
                    TimerForStopMovement = 0;
                    IsMoving = false;
                    return true;
                }
                else if (IsReachedDestination)
                {
                    TimerForStopMovement += Time.deltaTime;
                    if (TimerForStopMovement > MaxTimerForStopMovement)
                    {
                        IsReachedDestination = false;
                        agent.isStopped = true;
                        TimerForStopMovement = 0;                        
                        IsMoving = false;
                        return true;
                    }
                }
            }
            else
            {
                agent.stoppingDistance = 0;
            }

        }
        return false;
    }
    public string MoveToEnemy()
    {
        if (agent.velocity != Vector3.zero)
        {
            IsMoving = true;
            InstantlyTurn(agent.destination);
            if (IsMoving)
            {

                if (SoldierUnitCombat.GetCombatRangeEnemy() != null)
                {

                        IsReachedDestination = true;
                        agent.isStopped = true;
                        TimerForStopMovement = 0;
                        IsMoving = false;
                    return "enter_combat";
                }
                else if (SoldierUnitDetection.GetNearestEnemy() == null)
                {

                    IsReachedDestination = true;
                    agent.isStopped = true;
                    TimerForStopMovement = 0;
                    IsMoving = false;
                    return "enter_idle";
                }

                
            }
            

        }
        else
        {
            return "enter_idle";
        }
        return "";
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
