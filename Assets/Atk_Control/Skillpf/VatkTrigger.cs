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
            //���oFSMC�h����쪬�A�������K���ɶ��˼�

        }
        
    }
}
