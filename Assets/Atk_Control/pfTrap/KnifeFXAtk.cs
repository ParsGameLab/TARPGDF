using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeFXAtk : MonoBehaviour
{
    public float Dmg=30f;
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
   
            


        }

        
    }
}
