using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBillboarding : MonoBehaviour
{
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(mainCamera.transform);
        
    }
}
