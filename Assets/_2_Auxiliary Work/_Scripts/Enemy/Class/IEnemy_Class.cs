using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public abstract class IEnemy_Class : MonoBehaviour {

        protected HealthSystem ref_HealthSystem;

        protected virtual void Awake() {

        }

        protected virtual void Start() {

        }

        public virtual void UnderAttack(float damagePoint) {

        }
    }

}