using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class _2_StatHandler_UnityChan : MonoBehaviour {

        private _2_Subroutines subroutinesCommander;

        private HealthSystem healthSystem;
        private ManaSystem manaSystem;

        private _2_StatBarListener_UnityChan myStatBarListener;
        public int maxHealthPoint;
        private int currentHealthPoint;
        public int maxManaPoint;
        private int currentManaPoint;

        private void Awake() {
            myStatBarListener = 
                GameObject.Find("Unity-Chan!Canvas").
                transform.Find("StatHUD_Unity-Chan!").GetComponent<_2_StatBarListener_UnityChan>();
            subroutinesCommander = _2_Subroutines.Instance;
            subroutinesCommander.SetUnityChanStatHandler(this);
        }

        private void Start() {
            healthSystem = new HealthSystem(maxHealthPoint);
            myStatBarListener.SetHealthSystem(healthSystem);
            currentHealthPoint = maxHealthPoint;
            manaSystem = new ManaSystem(maxManaPoint);
            myStatBarListener.SetManaSystem(manaSystem);
            currentManaPoint = maxHealthPoint;
        }

        public void ExpendMana(float manaConsumedPoint) {
            manaSystem.CalculateManaPoint(manaConsumedPoint);
            currentManaPoint -= (int)manaConsumedPoint;
        }

        public void UnderAttack(IEnemy_Base enemy, float damagePoint) {
            healthSystem.Calculate_HealthPoint_Damage(damagePoint);
            currentHealthPoint -= (int)damagePoint;
        }
     
    }

}