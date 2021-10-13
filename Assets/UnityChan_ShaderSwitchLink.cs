using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_ShaderSwitchLink : MonoBehaviour
{
    Material mat;
    float t = 0.0f;
    public float x;

    public bool Portal;
    public bool Revival;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;        
    }

    void Update()
    {
        t += x * Time.deltaTime;
        //float S_Value = 10f - Mathf.Lerp(0f, 10f, t);

        if (Portal == true || Revival == true)
            mat.SetFloat("_AllPower", (10f - Mathf.Lerp(0f, 10f, t)));        

    }

    public void Close_Hit()
    {
        mat.SetFloat("_AllPower", 0f);
        GetComponent<UnityChan_ShaderSwitchLink>().enabled = false;
    }

    public void Close()
    {
        Portal = false;
        Revival = false;
        GetComponent<UnityChan_ShaderSwitchLink>().enabled = false;        
    }

    public void GetHit_SW()
    {
        if (Portal == false || Revival == false)
        {
            t = 0;
            mat = GetComponent<Renderer>().material;
            mat.SetFloat("_AllPower", 10f);
            Renderer rend = GetComponent<Renderer>();
            rend.material.SetColor("_InnerColor", new Color(130f / 255f, 0, 0, 0));
            Invoke("Close_Hit", 0.1f);
        }
    }

    public void Portal_SW()
    {
        t = 0;
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_AllPower", 10f);
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_InnerColor", new Color(55 / 255f, 70 / 255f, 120 / 255f, 0));
        Portal = true;
        Invoke("Close", 4f);
    }

    public void Revival_SW()
    {
        t = 0;
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_AllPower", 10f);
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_InnerColor", new Color(100 / 255f, 100 / 255f, 40 / 255f, 0));
        Revival = true;
        Invoke("Close", 4f);
    }
}
