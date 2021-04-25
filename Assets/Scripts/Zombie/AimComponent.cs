using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimComponent : MonoBehaviour
{
    private Transform aimTransform;

    private Vector2 desiredLookDirection;
    private Vector2 currentLookDirection;
    private SharedProperties lookSpeed;

    public void SetLookSpeed(SharedProperties lookSpeed)
    {
        this.lookSpeed = lookSpeed;
    }

    void Awake()
    {
        aimTransform = GetComponent<Transform>();
        if (lookSpeed == null)
        {
            lookSpeed = ScriptableObject.CreateInstance(typeof(SharedProperties)) as SharedProperties;
            lookSpeed.Value = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDesiredLookDirection(Vector2 newLookDirection)
    {
        desiredLookDirection = newLookDirection;
        if (newLookDirection == Vector2.right)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
		else
		{
            GetComponent<SpriteRenderer>().flipX = true;
        }
        
    }
}
