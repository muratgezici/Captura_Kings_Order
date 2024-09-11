using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFollowMouseTest : MonoBehaviour
{
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = 0;
        transform.position = mousePos;
    }
}
