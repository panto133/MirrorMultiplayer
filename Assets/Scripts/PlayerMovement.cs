using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine.AI;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private GameObject playerObject = null;

    [SerializeField] private Vector3 offsetPosition = new Vector3(0, 9f, 8f);
    [SerializeField] private Vector3 offsetRotation = new Vector3(30f, 180f, 0);

    private Camera cam;

    private void Start()
    {
        if (!hasAuthority) return;
        
        cam = Camera.main;
        cam.transform.position = offsetPosition;
        cam.transform.rotation = Quaternion.Euler(offsetRotation);
    }
    private void FixedUpdate()
    {
        if (!hasAuthority) return;

        MovePlayer();
        MoveCamera();
    }
    
    private void MoveCamera()
    {
        Camera.main.transform.position = playerObject.transform.position + offsetPosition;
    }
    
    private void MovePlayer()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
    }
}
