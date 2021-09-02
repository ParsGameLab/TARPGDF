using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public abstract class IEnemy_Base : IEnemy_Class {

        #region Field
        protected HealthBar myHealthBar;

        protected float currentHealthPoint;
        [SerializeField] protected float maxHealthPoint;
        #endregion

        #region Function
        protected override void Awake() {
            myHealthBar = transform.GetChild(0).transform.Find("HealthBar").GetComponent<HealthBar>();
        }

        protected override void Start() {
            ref_HealthSystem = new HealthSystem(maxHealthPoint);

            currentHealthPoint = maxHealthPoint;

            myHealthBar.SetUp_HealthSystem(ref_HealthSystem);

            myHealthBar.SetSize();
        }
        #endregion

        #region Method
        public override void UnderAttack(float damagePoint) {
            currentHealthPoint -= (int)damagePoint;
            ref_HealthSystem.Calculate_HealthPoint(damagePoint);
        }
        #endregion

    }

}
