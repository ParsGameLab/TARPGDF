using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStromEffect : MonoBehaviour
{
    public GameObject MagicStromFloorEffect;
    public GameObject MagicStromSmoke;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(MagicStromFloorEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        Instantiate(MagicStromSmoke, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
