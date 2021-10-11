using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldContral : MonoBehaviour
{
    Material mat;
    float t = 0.0f;

    // Start is called before the first frame update
    void Start()
    {       
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_AllPower", (2+Mathf.PingPong(Time.time * 15, 10)));
    }

    // Update is called once per frame
    void Update()
    {
        //t += 0.3f * Time.deltaTime;
        //mat.SetFloat("_AllPower", Mathf.Lerp(0f, 10f, t));        
        //mat.SetFloat("_AllPower", Mathf.PingPong(Time.time*15, 10));
        //mat.SetFloat("_AllPower",0f);  
    }
}
