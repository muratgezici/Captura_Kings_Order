using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CMoveArmyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> SELECTEDAGENTS = new List<GameObject>();
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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
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
                
                agentobj.GetComponent<CSoldierUnitManager>().MoveToDestination(hit);

               
            }
            
            
        }
    }
}
