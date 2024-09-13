using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CClickAndGetInfoForBottomUI : MonoBehaviour
{
    [SerializeField] private GameObject BuildingPanel;
    [SerializeField] private GameObject SoldierUnitPanel;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if (hit.transform.gameObject.CompareTag("Building"))
                {

                    BuildingPanel.SetActive(true);
                    BuildingPanel.GetComponent<CBuildingInfoToUI>().SetBuilding(hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("SoldierUnit"))
                {
                    BuildingPanel.SetActive(false);
                }
                else
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        BuildingPanel.SetActive(false);
                    }
                }
            }
        }
    }
}
