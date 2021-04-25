using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;



public class LightDiminish : MonoBehaviour
{

    public float maxLight = 100f;
    public float lightLeft = 100f;
    public Light2D lightS; 
    public float maxIntensity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lightLeft > 0.1)
        {
            lightLeft-=0.01f;
            lightS.intensity = maxIntensity * lightLeft / maxLight;
        }

        
    }

    public void Tick()
	{
		
	}
    public void refill()
	{
        lightLeft = maxLight;
	}

}
