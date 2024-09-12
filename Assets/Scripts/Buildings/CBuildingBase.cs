using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBuildingBase : MonoBehaviour
{
    //Colors: green, red, blue(player), yellow(neutral)
    [SerializeField] protected string BuildingName = "";
    [SerializeField] protected string OwnedByColor = "yellow";
    [SerializeField] protected float MaxTimeForProduction = 0;
    [SerializeField] protected List<GameObject> UnitsInsideTile = new List<GameObject>();
    [SerializeField] protected float TimeCounter = 0;
    [SerializeField] protected GameObject[] ExtraParticles;
    [SerializeField] protected GameObject[] OtherTeamBuildings;
    private void Start()
    {
        //For Mouse Testing Delete In Real Game and only use OnBuildingCapture
        EventManager.OnMouseClickToCapture += OnBuildingCapture;
        OnBuildingCapture(OwnedByColor);
        PlayExtraParticles();
    }
    public void PlayExtraParticles()
    {
        if (ExtraParticles.Length != 0)
        {
            if (OwnedByColor != "yellow")
            {
                foreach (GameObject obj in ExtraParticles)
                {
                    obj.GetComponent<ParticleSystem>().Play();
                }
            }
            else
            {
                foreach (GameObject obj in ExtraParticles)
                {
                    obj.GetComponent<ParticleSystem>().Stop();
                }
            }
        }
    }
    public void OnBuildingCapture(string team_color)
    {
        TimeCounter = 0;
        OwnedByColor = team_color;
        if(OwnedByColor == "yellow")
        {
            OtherTeamBuildings[1].SetActive(false);
            OtherTeamBuildings[2].SetActive(false);
            OtherTeamBuildings[3].SetActive(false);
            OtherTeamBuildings[0].SetActive(true);
            EventManager.TeamChangeAutomaticAnimations("yellow", gameObject);
        }
        else if (OwnedByColor == "blue")
        {
           
            OtherTeamBuildings[0].SetActive(false);
            OtherTeamBuildings[2].SetActive(false);
            OtherTeamBuildings[3].SetActive(false);
            OtherTeamBuildings[1].SetActive(true);
            EventManager.TeamChangeAutomaticAnimations("blue", gameObject);
        }
        else if (OwnedByColor == "red")
        {
            
            OtherTeamBuildings[1].SetActive(false);
            OtherTeamBuildings[2].SetActive(true);
            OtherTeamBuildings[3].SetActive(false);
            OtherTeamBuildings[0].SetActive(false);
            EventManager.TeamChangeAutomaticAnimations("red", gameObject);
        }
        else if (OwnedByColor == "green")
        {
            OtherTeamBuildings[1].SetActive(false);
            OtherTeamBuildings[0].SetActive(false);
            OtherTeamBuildings[2].SetActive(false);
            OtherTeamBuildings[3].SetActive(true);
            EventManager.TeamChangeAutomaticAnimations("green", gameObject);
        }
        PlayExtraParticles();
    }
}
