using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespowEvent : MonoBehaviour
{
    public GameObject Effect;
    public Transform StartPositionRotation;
    public float DestroyAfter = 2;


    public void Respown()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.Respown);
        var instance = Instantiate(Effect, StartPositionRotation.position, StartPositionRotation.rotation);
        Destroy(instance, DestroyAfter);

        this.gameObject.GetComponentInChildren<UnityChan_ShaderContral>().enabled = true;
        this.gameObject.GetComponentInChildren<UnityChan_ShaderContral>().Revival_SW();
    }

}
