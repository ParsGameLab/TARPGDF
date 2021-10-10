using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStunEvent : MonoBehaviour
{
    public GameObject Effect;
    public Transform StartPositionRotation;
    public float DestroyAfter = 2;

    
    public void MobStun()
    {
        var instance = Instantiate(Effect, StartPositionRotation.position, StartPositionRotation.rotation);
        Destroy(instance, DestroyAfter);
    }
    public void StunRecover()
    {
        transform.GetComponentInParent<AINormalMob>().m_Data.State= AIData.eMobState.normal;
    }
}
