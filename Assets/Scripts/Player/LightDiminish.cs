using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;



public class LightDiminish : MonoBehaviour
{

    public float maxLight = 100f;
    public float lightLeft = 100f;
    public Light2D lightS;
    public Light2D persLight;
    public float maxIntensity;
    public GameObject surface;
    public Light2D globalLight;
    public bool onSurface = true;

    // Start is called before the first frame update
    void Start()
    {
        globalLight.intensity = 5;
        persLight.intensity = 0;
        lightS.intensity = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameObject.transform.position.y < surface.transform.position.y)
		{
			if (onSurface)
			{
                
                onSurface = false;
            }

            if(globalLight.intensity != 0.58f)
			{
                globalLight.intensity = Mathf.Lerp(globalLight.intensity, 0.58f, Time.deltaTime);
            }
            if(persLight.intensity != 1)
			{
                persLight.intensity  = Mathf.Lerp(persLight.intensity , 1, Time.deltaTime);
            }

            

            if (lightLeft > 0.1 )
            {
                lightLeft -= 0.01f;
                lightS.intensity = maxIntensity * lightLeft / maxLight;
            }
        }
		else
		{
			if (!onSurface)
			{
                onSurface = true;
                globalLight.intensity = 5;
                persLight.intensity = 0;
                lightS.intensity = 0;
            }
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
