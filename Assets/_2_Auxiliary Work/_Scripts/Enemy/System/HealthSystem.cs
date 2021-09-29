using System;
using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class HealthSystem {

        public event EventHandler OnHealthChanged;
        public event EventHandler OnHealthEmpty;

        #region Field
        private int maxHealthPoint;
        private int currentHealthPoint;
        #endregion

        public HealthSystem(float theMaxHealthPoint) {
            maxHealthPoint = (int)theMaxHealthPoint;
            currentHealthPoint = maxHealthPoint;            
        }

        //#region Property
        //public float CurrentHealthPoint {
        //    get { return currentHealthPoint; }
        //}
        //#endregion

        #region Method
        public void ResetHealthPoint(float maxHealthPoint) {
            this.maxHealthPoint = (int)maxHealthPoint;
            currentHealthPoint = this.maxHealthPoint;

            OnHealthChanged?.Invoke(this, EventArgs.Empty);
        }

        public float GetHealthPercent() {
            return currentHealthPoint / (float)maxHealthPoint;
        }

        public void Calculate_HealthPoint_Damage(float theDamagePoint) {
            currentHealthPoint -= (int)theDamagePoint;
            if(currentHealthPoint <= 0) { 
                currentHealthPoint = 0;

                OnHealthEmpty?.Invoke(this, EventArgs.Empty);
            }

            OnHealthChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Calculate_HealthPoint_Heal(float theChargePoint) {
            currentHealthPoint += (int)theChargePoint;
            if(currentHealthPoint >= maxHealthPoint) {
                currentHealthPoint = maxHealthPoint;
            }

            OnHealthChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

    }

}
