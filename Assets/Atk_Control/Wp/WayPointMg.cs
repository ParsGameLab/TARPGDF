using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMg : MonoBehaviour
{
    // Start is called before the first frame update
    public PathArray[] patharray;

    [System.Serializable]
    public class PathArray
    {
        public List<Transform> wps= new List<Transform>();

    }
    public List<Transform> WayPointPath
    {
        get { return patharray[Random.Range(0, patharray.Length)].wps; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
