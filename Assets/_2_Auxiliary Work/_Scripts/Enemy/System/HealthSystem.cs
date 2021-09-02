using System;
using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class HealthSystem {

        #region Field
        public event EventHandler OnHealthChanged;


        private int currentHealthPoint;
        private int maxHealthPoint;
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

        public void Calculate_HealthPoint(float theDamageAmount) {
            currentHealthPoint -= (int)theDamageAmount;
            if(currentHealthPoint <= 0) { currentHealthPoint = 0; }

            OnHealthChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

    }

}
