using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicCorsshair : MonoBehaviour
{

    public RectTransform Corsshair; // The RecTransform of reticle UI element.


    [Range(100f, 150)]
    public float size;
    float t = 0.0f;


    public void Start()
    {

        Corsshair = GetComponent<RectTransform>();

    }

    public void Update()
    {
        // Check if player is currently moving and Lerp currentSize to the appropriate value.
        // Set the reticle's size to the currentSize value.

        Corsshair.sizeDelta = new Vector2(size, size);

        

        //t += 0.5f * Time.deltaTime;

        
        if (Input.GetMouseButtonDown(0))        
            size = 105;
        else if (Input.GetMouseButtonUp(1))
            size = 115;
        else if (size <= 100)
        {}
        else 
           size -= 20f * Time.deltaTime;



    }


}
