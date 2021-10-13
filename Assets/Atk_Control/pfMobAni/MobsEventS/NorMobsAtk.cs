using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorMobsAtk : MonoBehaviour
{
    public float dmg = 20.0f;
    public GameObject EnemyEffect;

    private void OnTriggerEnter(Collider other)
    {
        EnemyEffect.SetActive(true);

        if (other.gameObject.CompareTag("Player"))
        {

            other.GetComponent<_2_StatHandler_UnityChan>().UnderEnemyAttack(dmg);
            other.GetComponent<MobGetHitEvent>().GetHit();
            other.GetComponent<UnityChan_GetHitEvent>().UnityChan_GetHit();

        }
        if (other.gameObject.CompareTag("Core"))
        {
            other.GetComponent<_2_LevelCoreHandler>().UnderEnemyAttack(dmg);
        }

    }
}
