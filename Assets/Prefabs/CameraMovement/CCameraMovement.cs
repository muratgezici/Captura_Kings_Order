using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float RotationAmount;
    [SerializeField] private float TimeIntervalBetweenEachRotation;
    private float TickCounter = 0f;
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            if (gameObject.GetComponent<Camera>().orthographicSize > 4f)
            {
                gameObject.GetComponent<Camera>().orthographicSize--;
            }

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (gameObject.GetComponent<Camera>().orthographicSize < 18.5f)
            {
                gameObject.GetComponent<Camera>().orthographicSize++;
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            TickCounter += Time.deltaTime;
            if(TickCounter >= TimeIntervalBetweenEachRotation)
            {
                Vector3 currentRotation = transform.rotation.eulerAngles;
                float newYRotation = currentRotation.y + RotationAmount;
                transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
                TickCounter = 0f;
            }
            
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            TickCounter += Time.deltaTime;
            if (TickCounter >= TimeIntervalBetweenEachRotation)
            {
                Vector3 currentRotation = transform.rotation.eulerAngles;
                float newYRotation = currentRotation.y - RotationAmount;
                transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
                TickCounter = 0f;
            }
            
        }
        else
        {
            TickCounter = 0f;
        }
    }
    private void LateUpdate()
    {

        float cam_angle = -transform.eulerAngles.y * Mathf.Deg2Rad;
        Vector3 move_vector = new Vector3(Vector3.forward.x * (Mathf.Cos(cam_angle)) - Vector3.forward.z * (Mathf.Sin(cam_angle)), 0, Vector3.forward.z * (Mathf.Cos(cam_angle)) + Vector3.forward.x * (Mathf.Sin(cam_angle)));

        transform.position = Player.transform.position - move_vector * 3f + Vector3.up * (12f - 5f) - transform.forward * 5f;
       
    }
}
