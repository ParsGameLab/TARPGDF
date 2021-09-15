using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMg : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> wps= new List<Transform>();

    public List<Transform> WayPointPath
    {
        get { return wps; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
