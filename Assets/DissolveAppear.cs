using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveAppear : MonoBehaviour
{
    Material mat;
    float t;
    public float x;
    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        t += x * Time.deltaTime;           
        mat.SetFloat("_AdvancedDissolveCutoutStandardClip", (1-Mathf.Lerp(0f, 1f, t)));
    }

}
