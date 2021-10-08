using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppear : MonoBehaviour
{
    Material mat;
    float t;
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_AdvancedDissolveEdgeBaseWidthStandard", 1f);        
    }

    // Update is called once per frame
    void Update()
    {
        t += x * Time.deltaTime;
        mat.SetFloat("_AdvancedDissolveCutoutStandardClip", (1 - Mathf.Lerp(0f, 1f, t)));
    }
}
