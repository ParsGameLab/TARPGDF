using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveSphere : MonoBehaviour {

    public GameObject DissolveWeapon1;
    public GameObject DissolveWeapon2;
    public GameObject DieSmoke;
    public GameObject DissolveParticle;

    Material mat;
    float t = 0.0f;

    private void Start() 
    {
        Invoke("Die_Smoke", 0.7f);
        Invoke("Dissolve_Particle", 1.5f);

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

    private void Dissolve_Particle()
    {
        DissolveParticle.SetActive(true);
    }

    private void Die_Smoke()
    {
        DieSmoke.SetActive(true);
    }

}