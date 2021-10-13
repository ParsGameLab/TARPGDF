using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChan_GetHitEvent : MonoBehaviour
{
    public void UnityChan_GetHit()
    {
        this.gameObject.GetComponentInChildren<UnityChan_ShaderContral>().enabled = true;
        this.gameObject.GetComponentInChildren<UnityChan_ShaderContral>().GetHit_SW();
    }
}
