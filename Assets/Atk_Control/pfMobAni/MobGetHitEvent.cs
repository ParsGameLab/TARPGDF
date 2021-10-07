using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobGetHitEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] bleedposition;
    public GameObject[] Effect;
    public float DestroyAfter = 1.5f;
    public void GetHit()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.HitBody);
        Transform sposition = bleedposition[Random.Range(0, bleedposition.Length)];
        GameObject effect = Effect[Random.Range(0, Effect.Length)];
        GameObject instane=Instantiate(effect, sposition.position, Quaternion.identity);
        Destroy(instane, DestroyAfter);
    }
}
