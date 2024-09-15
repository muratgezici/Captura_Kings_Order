using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class CSoldierUnitManager : MonoBehaviour
{
    private enum SoldierUnitState
    {
        Idle,
        MoveToDestination,
        MoveToBasePosition,
        DetectEnemies,
        MoveToDetectedEnemy,
        CombatStart,
        CombatInProgress,
        CaptureBuildings,
    }

    [SerializeField] private SoldierUnitState _CurrentState;
    [SerializeField] private bool InStateCheck = false; //change when state chances

    [SerializeField] private CSoldierUnitMove SoldierUnitMoveScript;
    [SerializeField] private CSoldierUnitDetection SoldierUnitDetection;
    [SerializeField] private CSoldierUnitCombat SoldierUnitCombat;
    [SerializeField] private CSoldierUnitBase SoldierUnitBase;
    [SerializeField] private GameObject BasePositionObject;
    [SerializeField] private GameObject BottomCircleOnSelectedObject;

    private bool IsSoldierUnitSelected = false;
    private bool IsOnBasePosition = true;
    private GameObject NearestDetectedEnemy;
    private GameObject NearestCombatEnemy;

    public void SetIsSoldierUnitSelected(bool value)
    {
        IsSoldierUnitSelected = value;
        BottomCircleOnSelectedObject.SetActive(value);
    }
    public bool GetIsSoldierUnitSelected()
    {
        return IsSoldierUnitSelected;
    }
    private void Start()
    {
        _CurrentState = SoldierUnitState.Idle;
    }
    private void Update()
    {

        switch(_CurrentState)
        {
            case SoldierUnitState.Idle:
                
                if (!InStateCheck)
                {
                    //Debug.Log("in idle");
                    InStateCheck = true;
                }
                if (SoldierUnitCombat.GetCombatRangeEnemy() != null)
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.CombatStart;
                }
                else if(SoldierUnitDetection.GetNearestEnemy() != null)
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.DetectEnemies;
                }
                else if(SoldierUnitBase.GetOwnedByColor() != "blue" && !IsOnBasePosition)
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.MoveToBasePosition;
                }
                break;
            case SoldierUnitState.MoveToBasePosition:
                if (!InStateCheck)
                {
                    //Debug.Log("movetobase");
                    MoveToDestinationAfterChase(BasePositionObject);
                    InStateCheck = true;
                }
                if (SoldierUnitCombat.GetCombatRangeEnemy() != null)
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.CombatStart;
                }
                else if (SoldierUnitDetection.GetNearestEnemy() != null)
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.DetectEnemies;
                }
                else if (SoldierUnitMoveScript.MoveToClickedPos())
                {
                   InStateCheck = false;
                   IsOnBasePosition = true;
                  _CurrentState = SoldierUnitState.Idle;
                }
                
                break;
            case SoldierUnitState.MoveToDestination:
                if (!InStateCheck)
                {
                    //Debug.Log("movetodest");
                    InStateCheck = true;
                }
                if (SoldierUnitMoveScript.MoveToClickedPos())
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.Idle;
                }
                break;
            case SoldierUnitState.DetectEnemies:
                if (!InStateCheck)
                {
                    //Debug.Log("detect");
                    InStateCheck = true;
                }
                
                if(SoldierUnitDetection.GetNearestEnemy() != null)
                {
                    MoveToEnemyDetected(SoldierUnitDetection.GetNearestEnemy());
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.MoveToDetectedEnemy;
                }
                else //go to base position for enemy ai
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.Idle;


                }
                
                break;
            case SoldierUnitState.MoveToDetectedEnemy:
                if (!InStateCheck)
                {
                    //Debug.Log("in detect move");
                    IsOnBasePosition = false;
                    InStateCheck = true;
                }
                
                if (SoldierUnitDetection.GetNearestEnemy() != null)
                {
                    UpdateMoveToEnemyDestination(SoldierUnitDetection.GetNearestEnemy());
                }
                else
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.Idle;
                }
                string status = SoldierUnitMoveScript.MoveToEnemy();
                if (status == "enter_idle")
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.Idle;
                }
                else if (status == "enter_combat")
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.CombatStart;
                }
                break;
            case SoldierUnitState.CombatStart:
                if (!InStateCheck)
                {
                   // Debug.Log("combat");
                    InStateCheck = true;
                }
                if(SoldierUnitCombat.GetCombatRangeEnemy() != null)
                {
                    SoldierUnitCombat.InitiateCombat(SoldierUnitCombat.GetCombatRangeEnemy());

                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.CombatInProgress;
                }
                else
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.DetectEnemies;
                }

                
                break;
            case SoldierUnitState.CombatInProgress:
                if (!InStateCheck)
                {
                    //Debug.Log("combatprogress");
                    InStateCheck = true;
                }
                if (SoldierUnitCombat.InitiateCombat(SoldierUnitCombat.GetCombatRangeEnemy()))
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.CombatStart;
                }
                if (SoldierUnitCombat.GetCombatRangeEnemy() == null)
                {
                    InStateCheck = false;
                    _CurrentState = SoldierUnitState.DetectEnemies;
                }
                
                


                break;
            case SoldierUnitState.CaptureBuildings:
                if (!InStateCheck)
                {
                    //Debug.Log("capture");
                    InStateCheck = true;
                }

                break;

        }
    }

    public void MoveToDestination(RaycastHit hit)
    {
        //combat valuelarý da sýfýrlamalý
        SoldierUnitCombat.SetAttackTimer(0);
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = hit.point;
        SoldierUnitMoveScript.SetMoveToClickedPosition(hit.transform.gameObject);
        InStateCheck = false;
        _CurrentState = SoldierUnitState.MoveToDestination;
    }
    public void MoveToDestinationAfterProduced(GameObject hit)
    {
        //combat valuelarý da sýfýrlamalý
        SoldierUnitCombat.SetAttackTimer(0);
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = hit.transform.position;
        SoldierUnitMoveScript.SetMoveToClickedPosition(hit);
        InStateCheck = false;
        _CurrentState = SoldierUnitState.MoveToDestination;
    }
    public void MoveToDestinationAfterChase(GameObject hit)
    {
        //combat valuelarý da sýfýrlamalý
        SoldierUnitCombat.SetAttackTimer(0);
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = hit.transform.position;
        SoldierUnitMoveScript.SetMoveToClickedPosition(hit);
       
        
    }
    public void MoveToEnemyDetected(GameObject nearest_enemy)
    {
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = nearest_enemy.transform.position;
        SoldierUnitMoveScript.SetMoveToNearestEnemyPosition(nearest_enemy);
        

    }
    public void UpdateMoveToEnemyDestination(GameObject nearest_enemy)
    {
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.destination = nearest_enemy.transform.position;
    }

   

}
