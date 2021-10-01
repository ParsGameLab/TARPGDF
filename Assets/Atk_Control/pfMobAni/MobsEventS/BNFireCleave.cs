using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BNFireCleave : MonoBehaviour
{
    // Start is called before the first frame update
    public float dmg = 20.0f;
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Player"))
        {

            other.GetComponent<_2_StatHandler_UnityChan>().UnderEnemyAttack(dmg);
            
        }
        if (other.gameObject.CompareTag("Core"))
        {
            other.GetComponent<_2_LevelCoreHandler>().UnderEnemyAttack(dmg);
        }

    }
}
