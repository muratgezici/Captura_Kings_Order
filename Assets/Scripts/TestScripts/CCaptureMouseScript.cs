using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCaptureMouseScript : MonoBehaviour
{
    private int color = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (color == 0)
            {
                EventManager.MouseClickToCapture("blue");
                color = 1;
            }
            else if (color == 1)
            {
                EventManager.MouseClickToCapture("red");
                color = 2;
            }
            else if (color == 2)
            {
                EventManager.MouseClickToCapture("green");
                color = 3;
            }
            else if (color == 3)
            {
                EventManager.MouseClickToCapture("yellow");
                color = 0;
            }

        }
    }
}
