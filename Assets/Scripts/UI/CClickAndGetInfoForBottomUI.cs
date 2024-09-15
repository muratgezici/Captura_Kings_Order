using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class CClickAndGetInfoForBottomUI : MonoBehaviour
{
    [SerializeField] private GameObject BuildingPanel;
    [SerializeField] private GameObject MilitaryBuildingPanel;
    [SerializeField] private GameObject SoldierUnitPanel;
    private GameObject SelectedSoldier;
    private List<GameObject> Soldiers = new List<GameObject>();
    private float DoubleClickTimer = 0f;
    private float DoubleClickMaxTime = 0.1f;

    private float HoldClickTimer = 0f;
    private float HoldClickMaxTime = 0.1f;

    private bool isDoubleClickTimerActive = false;
    private bool IsSingleClickActivated = false;
    private bool IsMouse0Clicked = false;
    private bool IsMouse0Clicked_SecondCheck = false;
    private bool IsEnteredHoldMode = false;

    private Vector2 MousePosStartPoint;
    private Vector2 MousePosEndPoint;
    [SerializeField] RectTransform SelectionBox;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsMouse0Clicked = true;
            IsMouse0Clicked_SecondCheck = true;
            Debug.Log("Mousebtndown");
            
        }
        else if (Input.GetMouseButton(0) && IsEnteredHoldMode)
        {
            ResizeSelectionBox();
        }


        if(Input.GetMouseButtonUp(0))
        {
            if(IsEnteredHoldMode)
            {
                IsMouse0Clicked_SecondCheck = false;
            }
            else 
            {
                Debug.Log("Exited without entering Hold mode");
            }

            IsMouse0Clicked = false;
            IsEnteredHoldMode = false;
            HoldClickTimer = 0;

            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(false);
        }

        if (IsMouse0Clicked && !IsEnteredHoldMode && !isDoubleClickTimerActive)
        {
            Debug.Log("Entered Hold Mode TÝmer");
            HoldClickTimer += Time.deltaTime;
            if (HoldClickTimer > HoldClickMaxTime)
            {
                HoldClickTimer = 0;
                IsMouse0Clicked = false;
                IsEnteredHoldMode = true;
                IsMouse0Clicked_SecondCheck = false;
                Debug.Log("Entered Hold Mode");

                SelectionBox.sizeDelta = Vector2.zero;
                SelectionBox.gameObject.SetActive(true);
                MousePosStartPoint = Input.mousePosition;
            }
        }
        else if (IsMouse0Clicked_SecondCheck && !IsEnteredHoldMode) //Clicked with normal
        {

            if (!isDoubleClickTimerActive && !IsSingleClickActivated)
            {
                Debug.Log("DoubleClickStarted"+ "gameobject" + gameObject.name);
                isDoubleClickTimerActive = true;
            }
            else
            {
                if (isDoubleClickTimerActive && !IsSingleClickActivated)
                {
                    Debug.Log("DoubleClickExecuted"+"gameobject"+gameObject.name);
                    IsMouse0Clicked_SecondCheck = false;
                    isDoubleClickTimerActive = false;
                    DoubleClickTimer = 0;
                    Ray mousepos = Camera.main.ScreenPointToRay(Input.mousePosition);
                    DoDoubleClickActions(mousepos);

                }
            }
            
        }

        if (isDoubleClickTimerActive)
        {
            IsMouse0Clicked_SecondCheck = false;
            DoubleClickTimerHolder();
        }
        
    }
    public void DoubleClickTimerHolder()
    {
        DoubleClickTimer += Time.deltaTime;
        Debug.Log("DoubleClickTimer: " + DoubleClickTimer);
        if (DoubleClickTimer > DoubleClickMaxTime)
        {
            DoubleClickTimer = 0;
            isDoubleClickTimerActive = false;
            IsSingleClickActivated = true;
            EmptySoldierList();
            Debug.Log("SingleClickExecuted");
            IsMouse0Clicked_SecondCheck = false;
            Ray mousepos = Camera.main.ScreenPointToRay(Input.mousePosition);
            DoSingleClickOperations(mousepos);
        }
    }
    public void EmptySoldierList()
    {
        for (int i = Soldiers.Count - 1; i >= 0; i--)
        {
            if (Soldiers[i] == null)
            {
                Soldiers.Remove(Soldiers[i]);
            }
        }
        foreach (GameObject soldier_unit in Soldiers)
        {
            soldier_unit.GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(false);
        }
        Soldiers.Clear();
    }
    public void DoDoubleClickActions(Ray pos)
    {
        EmptySoldierList();
        RaycastHit hit;
        int layerMask = 1 << 6;
        if (Physics.Raycast(pos, out hit, 100, layerMask))
        {
            GameObject[] soldier_units = GameObject.FindGameObjectsWithTag("SoldierUnit");
            Soldiers.Add(hit.transform.gameObject);
            foreach (GameObject soldier_unit in  soldier_units)
            {
                if(soldier_unit.GetComponent<CSoldierUnitBase>().GetOwnedByColor() == hit.transform.gameObject.GetComponent<CSoldierUnitBase>().GetOwnedByColor())
                {
                    if (soldier_unit.GetComponent<CSoldierUnitBase>().GetSoldierName() == hit.transform.gameObject.GetComponent<CSoldierUnitBase>().GetSoldierName())
                    {
                        soldier_unit.GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(true);
                        Soldiers.Add(soldier_unit);
                    }
                }
                
            }
        }
        
    }
    public void DoSingleClickOperations(Ray pos)
    {
        RaycastHit hit;
        int layerMask = 1 << 8 | (1 << 9);
        layerMask = ~layerMask;
        if (Physics.Raycast(pos, out hit, 100, layerMask))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {

            }
            else
            {


                if (SelectedSoldier != null)
                {
                    SelectedSoldier.GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(false);
                    Debug.Log("soldier: " + SelectedSoldier.GetComponent<CSoldierUnitManager>().GetIsSoldierUnitSelected());
                    SelectedSoldier = null;
                }

                if (hit.transform.gameObject.CompareTag("Building"))
                {
                    MilitaryBuildingPanel.SetActive(false);
                    SoldierUnitPanel.SetActive(false);
                    BuildingPanel.SetActive(true);
                    BuildingPanel.GetComponent<CBuildingInfoToUI>().SetBuilding(hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("MilitaryBuilding"))
                {
                    BuildingPanel.SetActive(false);
                    SoldierUnitPanel.SetActive(false);
                    MilitaryBuildingPanel.SetActive(true);
                    MilitaryBuildingPanel.GetComponent<CMilitaryBuildingToUI>().SetBuilding(hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.CompareTag("SoldierUnit"))
                {
                    if (hit.transform.gameObject.GetComponent<CSoldierUnitBase>().GetOwnedByColor() == "blue")
                    {
                        BuildingPanel.SetActive(false);
                        MilitaryBuildingPanel.SetActive(false);
                        SoldierUnitPanel.SetActive(true);
                        //SoldierUnitPanel.GetComponent<CSoldierUnitToUI>().SetBuilding(hit.transform.gameObject);

                        SelectedSoldier = hit.transform.gameObject;

                        SelectedSoldier.GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(true);
                    }
                    

                }
                else
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        BuildingPanel.SetActive(false);
                        MilitaryBuildingPanel.SetActive(false);
                        SoldierUnitPanel.SetActive(false);
                    }
                }
            }
        }
        IsSingleClickActivated = false;


    }
    private void ResizeSelectionBox()
    {
        EmptySoldierList();
        float width = Input.mousePosition.x - MousePosStartPoint.x;
        float height = Input.mousePosition.y - MousePosStartPoint.y;
        SelectionBox.anchoredPosition = MousePosStartPoint + new Vector2(width/2, height/2);
        SelectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        Bounds bounds = new Bounds(SelectionBox.anchoredPosition, SelectionBox.sizeDelta);
        GameObject[] soldier_units = GameObject.FindGameObjectsWithTag("SoldierUnit");
        for (int i = 0; i< soldier_units.Length; i++)
        {
            //if units under selectionbox and are team blue choose them
            if (UnitsInSelectionBox(Camera.main.WorldToScreenPoint(soldier_units[i].transform.position), bounds))
            {
                if (soldier_units[i].GetComponent<CSoldierUnitBase>().GetOwnedByColor() == "blue")
                {
                    Soldiers.Add(soldier_units[i]);
                    soldier_units[i].GetComponent<CSoldierUnitManager>().SetIsSoldierUnitSelected(true);
                    Debug.Log("Selected");
                }
            }
        }
    }
    private bool UnitsInSelectionBox(Vector2 Position, Bounds bounds)
    {
        return Position.x > bounds.min.x && Position.x < bounds.max.x && Position.y > bounds.min.y && Position.y < bounds.max.y;
    }
}
