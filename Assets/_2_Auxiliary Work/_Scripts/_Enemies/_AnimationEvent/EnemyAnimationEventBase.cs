using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public abstract class EnemyAnimationEventBase : MonoBehaviour {

        private IEnemy_Base enemyBase;

        private void Awake() {
            enemyBase = transform.parent.GetComponent<IEnemy_Base>();
        }

        public void Attack() {
            enemyBase.Attack();
        }

    }

}
