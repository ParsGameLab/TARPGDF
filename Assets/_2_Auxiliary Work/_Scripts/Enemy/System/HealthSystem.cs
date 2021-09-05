using System;
using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class HealthSystem {

        #region Field
        public event EventHandler OnHealthChanged;
        public event EventHandler OnHealthCharging;

        private int currentHealthPoint;
        private int maxHealthPoint;

        private bool isWoodendummy = false;
        #endregion

        #region Constructor
        public HealthSystem(float theMaxHealthPoint) {
            maxHealthPoint = (int)theMaxHealthPoint;
            currentHealthPoint = maxHealthPoint;
        }
        #endregion

        #region Property
        public float CurrentHealthPoint {
            get { return currentHealthPoint; }
        }
        #endregion

        #region Method
        public float GetHealthPercent() {
            return currentHealthPoint / (float)maxHealthPoint;
        }

        public void Calculate_HealthPoint_Damage(float theDamagePoint, bool checkIsWoodendummy) {
            isWoodendummy = checkIsWoodendummy;
            currentHealthPoint -= (int)theDamagePoint;
            if(currentHealthPoint <= 0) { 
                currentHealthPoint = 0;
                HasBeganCharging();
            }
            OnHealthChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Calculate_HealthPoint_Charge(float theChargePoint) {
            currentHealthPoint += (int)theChargePoint;
            if(currentHealthPoint >= maxHealthPoint) {
                currentHealthPoint = maxHealthPoint;
            }
            OnHealthChanged?.Invoke(this, EventArgs.Empty);

        }

        public void HasBeganCharging() {
            if(currentHealthPoint <= 0 && isWoodendummy) {
                OnHealthCharging?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion

    }

}
