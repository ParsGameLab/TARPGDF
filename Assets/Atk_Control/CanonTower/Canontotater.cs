using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canontotater : MonoBehaviour
{
    private float speed;
    private void Start()
    {
        speed = GetComponentInParent<CanonAi>().fowardspeed;
    }
    void Update()
    {
        Vector3 Dir = GetComponentInParent<CanonAi>().GetUpDown();
        transform.Rotate(Dir * speed * Time.deltaTime);
    }
}
