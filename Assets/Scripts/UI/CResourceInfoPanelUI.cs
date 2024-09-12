using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CResourceInfoPanelUI : MonoBehaviour
{
    public void OnFoodHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0,140f,0);
        Debug.Log("hovered food");
    }
    public void OnWoodHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0, 140f, 0);
        Debug.Log("hovered food");
    }
    public void OnIronHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0, 140f, 0);
        Debug.Log("hovered food");
    }
    public void OnCoinHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0, 140f, 0);
        Debug.Log("hovered food");
    }
    public void OnMoraleHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0, 140f, 0);
        Debug.Log("hovered food");
    }
    public void OnPopulationHover(GameObject hovered)
    {
        gameObject.SetActive(true);
        transform.position = hovered.transform.position - new Vector3(0, 140f, 0);
        Debug.Log("hovered food");
    }
    public void OnExit()
    {
        gameObject.SetActive(false);
    }
}
