using iii_UMVR06_TPSDefenseGame_Subroutines_2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VatkTrigger : MonoBehaviour
{
    public float dmg = 20.0f;
    private void OnEnable()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.MagicSlash);
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("EnemyHit"))
        {

            other.GetComponentInParent<IEnemy_Base>().UnderAttack(dmg);
            other.GetComponentInParent<IEnemy_Base>().PlayGetHit();
            //Destroy(other.gameObject);
            //取得FSMC去把全域狀態切換順便給時間倒數

        }
        
    }
}
