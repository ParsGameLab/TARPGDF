using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAtkTrigger : MonoBehaviour
{
    public float Dmg = 60f;
    private void OnTriggerEnter(Collider other)
    {


        //if (other.gameObject.CompareTag("Enemy"))
        //{

        //    other.GetComponent<IEnemy_Base>().UnderAttack(Dmg);
        //    //Destroy(other.gameObject);
        //    Destroy(gameObject);
        //}
        if (other.gameObject.CompareTag("EnemyHit"))
        {
            
            other.GetComponentInParent<IEnemy_Base>().UnderAttack(Dmg);
            other.GetComponentInParent<AINormalMob>().m_Data.State = AIData.eMobState.dizzy;


        }


    }

}
