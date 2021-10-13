using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_ShaderContral : MonoBehaviour
{
    Material mat;
    float t = 0.0f;
    public float x;

    public GameObject Switch_Link1;
    public GameObject Switch_Link2;
    public GameObject Switch_Link3;
    public GameObject Switch_Link4;
    public GameObject Switch_Link5;
    public GameObject Switch_Link6;
    public GameObject Switch_Link7;
    public GameObject Switch_Link8;
    public GameObject Switch_Link9;
    public GameObject Switch_Link10;

    public bool Portal;
    public bool Revival;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;        
    }
    // Update is called once per frame
    void Update()
    {
        Switch_Link1.GetComponent<UnityChan_ShaderSwitchLink>().enabled = true;
        Switch_Link2.GetComponent<UnityChan_ShaderSwitchLink>().enabled = true;
        Switch_Link3.GetComponent<UnityChan_ShaderSwitchLink>().enabled = true;
        Switch_Link4.GetComponent<UnityChan_ShaderSwitchLink>().enabled = true;
        Switch_Link5.GetComponent<UnityChan_ShaderSwitchLink>().enabled = true;
        Switch_Link6.GetComponent<UnityChan_ShaderSwitchLink>().enabled = true;
        Switch_Link7.GetComponent<UnityChan_ShaderSwitchLink>().enabled = true;
        Switch_Link8.GetComponent<UnityChan_ShaderSwitchLink>().enabled = true;
        Switch_Link9.GetComponent<UnityChan_ShaderSwitchLink>().enabled = true;
        Switch_Link10.GetComponent<UnityChan_ShaderSwitchLink>().enabled = true;


        t += x * Time.deltaTime;
        //float S_Value = 10f - Mathf.Lerp(0f, 10f, t);

        if (Portal == true || Revival == true)
            mat.SetFloat("_AllPower", (10f - Mathf.Lerp(0f, 10f, t)));

        
        

        /*
        mat.SetFloat("_AllPowerGetHit", Mathf.PingPong(Time.time * 2, 10f));
        mat.SetFloat("_AllPowerPortal", Mathf.PingPong(Time.time * 2, 10f));
        mat.SetFloat("_AllPowerRevival", Mathf.PingPong(Time.time * 2, 10f));

        t += x * Time.deltaTime;
        float DissolveValue = 1 - Mathf.Lerp(0f, 0.7f, t);

        mat.SetFloat("_AdvancedDissolveCutoutStandardClip", (DissolveValue));

        if (DissolveValue == 0.3f)
            mat.SetFloat("_AdvancedDissolveCutoutStandardClip", (1 - Mathf.Lerp(0.7f, 1f, t / 4)));
        */
    }

    public void Close_Hit()
    {
        mat.SetFloat("_AllPower", 0f);
        GetComponent<UnityChan_ShaderContral>().enabled = false;
    }
    public void Close()
    {
        Portal = false;
        Revival = false;
        GetComponent<UnityChan_ShaderContral>().enabled = false;        
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

            Switch_Link1.GetComponent<UnityChan_ShaderSwitchLink>().GetHit_SW();
            Switch_Link2.GetComponent<UnityChan_ShaderSwitchLink>().GetHit_SW();
            Switch_Link3.GetComponent<UnityChan_ShaderSwitchLink>().GetHit_SW();
            Switch_Link4.GetComponent<UnityChan_ShaderSwitchLink>().GetHit_SW();
            Switch_Link5.GetComponent<UnityChan_ShaderSwitchLink>().GetHit_SW();
            Switch_Link6.GetComponent<UnityChan_ShaderSwitchLink>().GetHit_SW();
            Switch_Link7.GetComponent<UnityChan_ShaderSwitchLink>().GetHit_SW();
            Switch_Link8.GetComponent<UnityChan_ShaderSwitchLink>().GetHit_SW();
            Switch_Link9.GetComponent<UnityChan_ShaderSwitchLink>().GetHit_SW();
            Switch_Link10.GetComponent<UnityChan_ShaderSwitchLink>().GetHit_SW();
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

        Switch_Link1.GetComponent<UnityChan_ShaderSwitchLink>().Portal_SW();
        Switch_Link2.GetComponent<UnityChan_ShaderSwitchLink>().Portal_SW();
        Switch_Link3.GetComponent<UnityChan_ShaderSwitchLink>().Portal_SW();
        Switch_Link4.GetComponent<UnityChan_ShaderSwitchLink>().Portal_SW();
        Switch_Link5.GetComponent<UnityChan_ShaderSwitchLink>().Portal_SW();
        Switch_Link6.GetComponent<UnityChan_ShaderSwitchLink>().Portal_SW();
        Switch_Link7.GetComponent<UnityChan_ShaderSwitchLink>().Portal_SW();
        Switch_Link8.GetComponent<UnityChan_ShaderSwitchLink>().Portal_SW();
        Switch_Link9.GetComponent<UnityChan_ShaderSwitchLink>().Portal_SW();
        Switch_Link10.GetComponent<UnityChan_ShaderSwitchLink>().Portal_SW();
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

        Switch_Link1.GetComponent<UnityChan_ShaderSwitchLink>().Revival_SW();
        Switch_Link2.GetComponent<UnityChan_ShaderSwitchLink>().Revival_SW();
        Switch_Link3.GetComponent<UnityChan_ShaderSwitchLink>().Revival_SW();
        Switch_Link4.GetComponent<UnityChan_ShaderSwitchLink>().Revival_SW();
        Switch_Link5.GetComponent<UnityChan_ShaderSwitchLink>().Revival_SW();
        Switch_Link6.GetComponent<UnityChan_ShaderSwitchLink>().Revival_SW();
        Switch_Link7.GetComponent<UnityChan_ShaderSwitchLink>().Revival_SW();
        Switch_Link8.GetComponent<UnityChan_ShaderSwitchLink>().Revival_SW();
        Switch_Link9.GetComponent<UnityChan_ShaderSwitchLink>().Revival_SW();
        Switch_Link10.GetComponent<UnityChan_ShaderSwitchLink>().Revival_SW();
    }
}
