using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveSphere : MonoBehaviour {

    Material mat;
    float t = 0.0f;

    private void Start() {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        t += 0.3f * Time.deltaTime;
        mat.SetFloat("_AdvancedDissolveCutoutStandardClip", Mathf.Lerp(0f, 1f, t));
    }     
   
}