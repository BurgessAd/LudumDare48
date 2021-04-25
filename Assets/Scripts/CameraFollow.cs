using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //the follow will be on the crosshair (scaled down)
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform pickPos;

    [Range(0,1)] [SerializeField] private float smoothedPlayerSpeed = 0.125f;
    [Range(0,1)] [SerializeField] private float smoothedPickSpeed = 0.500f;
    [Range(0,0.010f)] [SerializeField] private float reach = 0.005f; 
    [SerializeField] private Vector3 offset; 

    
    void FixedUpdate()
    {
        //camera will follow the player but also move slightly towards the crosshair
        Vector3 initialDesiredPos = playerPos.position + offset;
        Vector3 smoothedPlayerPos = Vector3.Lerp(transform.position, initialDesiredPos, smoothedPlayerSpeed);

        Vector3 desiredPos = smoothedPlayerPos + reach*(pickPos.position - playerPos.position);
        Vector3 smoothedPickPos = Vector3.Lerp(transform.position, desiredPos, smoothedPickSpeed);

        transform.position = smoothedPickPos;
    }
}
