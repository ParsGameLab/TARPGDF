using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VatkTrigger : MonoBehaviour
{
    public float dmg = 20.0f;
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Enemy"))
        {

            other.GetComponent<IEnemy_Base>().UnderAttack(dmg);
            //Destroy(other.gameObject);
            //取得FSMC去把全域狀態切換順便給時間倒數
            
        }
        
    }
}
