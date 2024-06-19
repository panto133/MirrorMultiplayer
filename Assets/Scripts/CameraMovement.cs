using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    /// <summary>
    /// Bool variable that checks if an object is instantiated so
    /// that the camera can call FixedUpdate to always stick
    /// with the player.
    /// </summary>
   /* private bool isInstantiated = false;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private Vector3 offset;
    ///<summary>
    ///Function called by instantiated player to pin camera to
    ///a player so that the cam can move in the same direction
    ///as the player does while having the FOV.
    ///</summary>
    private void FixedUpdate()
    {
        if (isInstantiated) PinCamera();
    }
    /// <summary>
    /// Called by an instantiated player OnAwake to assign player variable
    /// only once to save performance.
    /// </summary>
    public void AssignPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isInstantiated = true;
    }
    public void PinCamera()
    {
        try
        {
            mainCam.transform.position = player.transform.position + offset;
        }
        catch
        {
            isInstantiated = false;
            return;
        }
    }*/
}
