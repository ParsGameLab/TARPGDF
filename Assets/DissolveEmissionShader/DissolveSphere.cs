using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveSphere : MonoBehaviour {

    public GameObject DissolveWeapon1;
    public GameObject DissolveWeapon2;

    Material mat;
    float t = 0.0f;

    private void Start() 
    {
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_AdvancedDissolveEdgeBaseWidthStandard", 0.1f);
        if (DissolveWeapon1 != null)
        {
            DissolveWeapon1.GetComponent<DissolveWeapon>().enabled = true;
            if(DissolveWeapon2 != null)
                DissolveWeapon2.GetComponent<DissolveWeapon>().enabled = true;
        }
    }

    private void Update()
    {
        t += 0.3f * Time.deltaTime;
        mat.SetFloat("_AdvancedDissolveCutoutStandardClip", Mathf.Lerp(0f, 1f, t));
    }     
   
}