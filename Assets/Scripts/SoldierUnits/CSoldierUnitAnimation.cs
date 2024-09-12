using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoldierUnitAnimation : MonoBehaviour
{
    [SerializeField] GameObject[] AnimatedAttackObjects;
   public void AnimateAttackAnimation()
    {
        foreach(var obj in AnimatedAttackObjects)
        {
            obj.GetComponent<MMF_Player>().PlayFeedbacks();
        }
    }
}
