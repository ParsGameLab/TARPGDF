using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_GetHit : MonoBehaviour
{
    Material mat;
    float t = 0.0f;
    public float x;

    // Start is called before the first frame update
    void Start()
    {

        mat = GetComponent<Renderer>().material;

        mat.SetFloat("_AllPowerGetHit", 10);
        Invoke("CloseShader", 0.2f);
    }

    void Update()
    {
        t += x * Time.deltaTime;
        float ShaderValue = Mathf.Lerp(10f, 0f, t);
        mat.SetFloat("_AllPowerGetHit", ShaderValue);
    }
    public void CloseShader()
    {
        this.gameObject.GetComponent<UnityChan_GetHit>().enabled = false;
    }
}
