using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CMoveArmyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> SELECTEDAGENTS = new List<GameObject>();
    [SerializeField] private GameObject Target;
    private void Update()
    {
        
        
        if (Input.GetMouseButtonDown(1))
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("SoldierUnit"))
            {
                if (obj.GetComponent<CSoldierUnitManager>().GetIsSoldierUnitSelected())
                {
                    SELECTEDAGENTS.Add(obj);
                }
            }
            MoveSelectedAgents();
        }
    }

    public void MoveSelectedAgents() 
    {
        RaycastHit hit;
        int layerMask = 1 << 10;
        //layerMask = ~layerMask;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, layerMask))
        {
            if(!hit.transform.CompareTag("Hexagon"))
            {
                return;
            }
            for (int i = SELECTEDAGENTS.Count - 1; i >= 0; i--)
            {
                if (SELECTEDAGENTS[i] == null || !SELECTEDAGENTS[i].GetComponent<CSoldierUnitManager>().GetIsSoldierUnitSelected())
                {
                    SELECTEDAGENTS.Remove(SELECTEDAGENTS[i]);

                }
            }
            foreach (var agentobj in SELECTEDAGENTS)
            {
                if(agentobj == null)
                {
                    continue;
                }
                Target.transform.position = hit.transform.position;
                agentobj.GetComponent<CSoldierUnitManager>().MoveToDestination(hit);


               
            }
            
            
        }
    }
}
