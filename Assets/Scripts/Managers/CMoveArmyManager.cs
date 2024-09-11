using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CMoveArmyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] ALLAGENTS;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveAllAgents();
        }
    }

    public void MoveAllAgents() //Use only for testing purposes!
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            foreach (var agentobj in ALLAGENTS)
            {
                agentobj.GetComponent<CSoldierAgentMove>().SetDestinationHexagon(hit.transform.gameObject);
                NavMeshAgent agent = agentobj.GetComponent<NavMeshAgent>();
                agent.destination = hit.point;
               
            }
            
        }
    }
}
