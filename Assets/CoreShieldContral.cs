using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreShieldContral : MonoBehaviour
{
    Material mat;
    float t = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;        
    }

    // Update is called once per frame
    void Update()
    {
        //t += 0.3f * Time.deltaTime;
        mat.SetFloat("_AllPower", (0.5f + Mathf.PingPong(Time.time*2, 1.5f)));
    }
}
