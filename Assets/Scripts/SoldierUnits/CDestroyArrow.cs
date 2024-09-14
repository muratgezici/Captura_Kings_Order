using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDestroyArrow : MonoBehaviour
{
    float Timer = 0;
    float DeathTime =0.6f;
    [SerializeField] bool IsDeathEnabled = false;
    private GameObject Target;
    // Update is called once per frame
    void Update()
    {

        if (!IsDeathEnabled)
        {
            return;
        }
        if (Target != null)
        {
            transform.LookAt(Target.transform);
        }
        
        Timer += Time.deltaTime;
        if(Timer > DeathTime)
        {
            Destroy(gameObject);
        }
    }
    public void SetIsDeathEnabled(bool val)
    {
        IsDeathEnabled = val;
    }
    public void LookAtTarget(GameObject obj)
    {
        Target = obj;
    }
}
