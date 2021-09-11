using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public abstract class IEnemyBehaviourSystem_Base {

        protected IEnemy_Base reference_IEnemyBaseTypeEnemy;

        protected RaycastHit[] results = new RaycastHit[5];

        public virtual void EnterState(IEnemy_Base theEnemy) {
            reference_IEnemyBaseTypeEnemy = theEnemy;
        }

        public virtual void DetectPlayer() {
            
        }

        public virtual void Update() {

        }

    }

}