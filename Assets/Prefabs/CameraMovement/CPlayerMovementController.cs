using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class CPlayerMovementController : MonoBehaviour
{
    private bool SingleTimeMovementChangeCheck = false;
    [SerializeField]
    private InputActionAsset InputActions;
    private InputActionMap PlayerActionMap;
    private InputAction Movement;

    [SerializeField]
    private Camera Camera;
    private NavMeshAgent Agent;

    [SerializeField]
    private float TargetLerpSpeed = 1;

    [SerializeField]
    [Range(0, 0.99f)]
    private float Smoothing = 0.25f;

    private Vector3 TargetDirection;
    private float LerpTime = 0;
    
    private Vector3 LastDirection;
    private Vector3 MovementVector;
    private Vector3 RotatedMovementVector;

    private bool IsGameStopped = false;
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        PlayerActionMap = InputActions.FindActionMap("Player");
        Movement = PlayerActionMap.FindAction("Move");
        Movement.started += HandleMovementAction;
        Movement.canceled += HandleMovementAction;
        Movement.performed += HandleMovementAction;
        Movement.Enable();
        PlayerActionMap.Enable();
        InputActions.Enable();
    }

    private void HandleMovementAction(InputAction.CallbackContext Context)
    {
        Vector2 input = Context.ReadValue<Vector2>();
        MovementVector = new Vector3(input.x,0,input.y);
        

    }
    private void Update()
    {

        if (IsGameStopped)
        {
            return;
        }

        MovementVector.Normalize();

        float cam_angle = -Camera.transform.eulerAngles.y * Mathf.Deg2Rad;
        RotatedMovementVector = new Vector3(MovementVector.x * (Mathf.Cos(cam_angle)) - MovementVector.z * (Mathf.Sin(cam_angle)), 0, MovementVector.z * (Mathf.Cos(cam_angle)) + MovementVector.x * (Mathf.Sin(cam_angle)));

        if (RotatedMovementVector != LastDirection)
        {
            LerpTime = 0;
        }
        LastDirection = RotatedMovementVector;


        TargetDirection = Vector3.Lerp(TargetDirection, RotatedMovementVector, Mathf.Clamp01(LerpTime* TargetLerpSpeed * (1-Smoothing)));
        
        Agent.Move(TargetDirection * Agent.speed * Time.deltaTime);
        Vector3 LookDirection = RotatedMovementVector;
        
        if (LookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(LookDirection), Mathf.Clamp01(LerpTime * TargetLerpSpeed * (1 - Smoothing)));

        }
        LerpTime += Time.deltaTime;
        if (MovementVector == new Vector3(0,0,0) && !SingleTimeMovementChangeCheck)
        {

            SingleTimeMovementChangeCheck = true;
        }
        else if(MovementVector != new Vector3(0, 0, 0) && SingleTimeMovementChangeCheck)
        {
 
            SingleTimeMovementChangeCheck = false;
        }
    }
    private void LateUpdate()
    {
        Camera.transform.position = transform.position-new Vector3(0,0,3f) + Vector3.up * (12f-5f);
    }
    private void SetIsGameStopped(bool val)
    {
        IsGameStopped = val;
    }
}
    
