using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public abstract class IEnemy_Base : MonoBehaviour {

        #region Field
        protected HealthSystem reference_HealthSystem;
        protected HealthBar myHealthBar;

        protected float currentHealthPoint;
        [SerializeField] protected float maxHealthPoint;

        public bool isWoodendummy = true;
        private bool isInvincible = false;

        private float delayTimer = 0;
        #endregion

        #region Function
        protected virtual void Awake() {
            myHealthBar = transform.GetChild(0).transform.Find("HealthBar").GetComponent<HealthBar>();
        }

        protected virtual void Start() {
            reference_HealthSystem = new HealthSystem(maxHealthPoint);

            currentHealthPoint = maxHealthPoint;

            myHealthBar.SetUp_HealthSystem(reference_HealthSystem);
            myHealthBar.SetSize();
            myHealthBar.Hide();
            reference_HealthSystem.OnHealthCharging += Reference_HealthSystem_OnHealthCharging;
        }

        protected virtual void Update() {
            if(isInvincible) {
                delayTimer = 0;
                Invoke("BeginCharge", .75f);
            } else {
                delayTimer += Time.deltaTime;
            }
        }
        #endregion

        #region Method
        public virtual void UnderAttack(float damagePoint) {
            if(isInvincible || delayTimer < 1f) {
                return;
            }

            if(myHealthBar.BarSpriteColor != Color.red) {
                myHealthBar.SetColor(Color.red);
            }

            currentHealthPoint -= (int)damagePoint;
            reference_HealthSystem.Calculate_HealthPoint_Damage(damagePoint, isWoodendummy);
            myHealthBar.Show();
        }

        protected virtual void ChargeHealth(float chargePoint) {
            currentHealthPoint += (int)chargePoint;
            if(currentHealthPoint % 1f == 0) {
                myHealthBar.SetColor(Color.green);
            }

            reference_HealthSystem.Calculate_HealthPoint_Charge(chargePoint);

            if(currentHealthPoint >= maxHealthPoint) {
                currentHealthPoint = maxHealthPoint;
                myHealthBar.Hide();
                isInvincible = false;
            }
        }

        private void BeginCharge() {
            ChargeHealth(Time.deltaTime * 175f);            
        }
        #endregion

        #region Event
        private void Reference_HealthSystem_OnHealthCharging(object sender, System.EventArgs e) {
            isInvincible = true;
        }
        #endregion

    }

}
