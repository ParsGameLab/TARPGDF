using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_Revival : MonoBehaviour
{
    Material mat;
    float t = 0.0f;
    public float x;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
