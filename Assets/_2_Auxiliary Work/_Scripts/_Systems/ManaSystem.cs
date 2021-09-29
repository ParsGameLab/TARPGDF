using System;
using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class ManaSystem {

        public event EventHandler OnManaChanged;

        private int maxManaPoint;
        private int currentManaPoint;

        public ManaSystem(float maxManaPoint) {
            this.maxManaPoint = (int)maxManaPoint;
            currentManaPoint = this.maxManaPoint;
        }

        public void ResetManaPoint(float maxManaPoint) {
            this.maxManaPoint = (int)maxManaPoint;
            currentManaPoint = this.maxManaPoint;
            OnManaChanged?.Invoke(this, EventArgs.Empty);
        }

        public float GetManaPercent() {
            float currentManaPercent = currentManaPoint / (float)maxManaPoint;
            return currentManaPercent;
        }

        public void CalculateManaPoint(float manaConsumedPoint) {
            currentManaPoint -= (int)manaConsumedPoint;
            if(currentManaPoint <= 0) { currentManaPoint = 0; }
            OnManaChanged?.Invoke(this, EventArgs.Empty);
        }
        public void RecoverManaPercent(float manaRecoverPoint)
        {
            currentManaPoint += (int)manaRecoverPoint;
            if(currentManaPoint >= maxManaPoint) { currentManaPoint = maxManaPoint; }
            OnManaChanged?.Invoke(this, EventArgs.Empty);

        }
        public bool TrySpendManaAmount(int spendManaAmount)
        {
            if( GetManaPoint() >= spendManaAmount)
            {
                currentManaPoint -= spendManaAmount;
                OnManaChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetManaPoint()
        {
            return currentManaPoint;
        }
        public bool IsManaEnough(int spendMana)
        {
            if (GetManaPoint() >= spendMana)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
