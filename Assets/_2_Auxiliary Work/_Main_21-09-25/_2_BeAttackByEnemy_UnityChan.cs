using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class _2_BeAttackByEnemy_UnityChan : MonoBehaviour {

        private void OnTriggerExit(Collider other) {            
            if(other.gameObject.CompareTag("EnemyWeapon")) {
                other.GetComponent<_2_EnemyWeaponHitPoint>().MyTopperParent.GetComponent<IEnemy_Base>().Attack();
            }
        }

    }

}
