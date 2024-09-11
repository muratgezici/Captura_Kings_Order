using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAutomaticallyPlayAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.GetComponent<MMF_Player>().PlayFeedbacks();
             
        EventManager.OnTeamChangeAutomaticAnimations += PlayAnimations;
    }
    public void PlayAnimations(string team_color, GameObject parent)
    {
        
        if (transform.IsChildOf(parent.transform))
        {
            if (team_color != "yellow")
            {
                Debug.Log("aadadssbbss: " + team_color);
                gameObject.GetComponent<MMF_Player>().ResetFeedbacks();
                gameObject.GetComponent<MMF_Player>().PlayFeedbacks();
               
            }
            else
            {
                Debug.Log("aadadsabbss: " + team_color);
                gameObject.GetComponent<MMF_Player>().StopFeedbacks();
            }
        }
        
        
    }
}
   
