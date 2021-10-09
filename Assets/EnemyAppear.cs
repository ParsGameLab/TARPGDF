using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppear : MonoBehaviour
{
    Material mat;
    float t;
    private float x = 2f;
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
        float DissolveValue = 1 - Mathf.Lerp(0f, 0.7f, t);

        mat.SetFloat("_AdvancedDissolveCutoutStandardClip", (DissolveValue));

        if(DissolveValue == 0.3f)       
            mat.SetFloat("_AdvancedDissolveCutoutStandardClip",(1 - Mathf.Lerp(0.7f, 1f, t/4)));
    }
}
