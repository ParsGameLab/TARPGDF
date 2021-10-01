using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 { 

    public class _2_LevelCoreHandler : MonoBehaviour {

        [Tooltip("核心受擊後的反震力")]
        public int backShockPower = 100;
        [Min(1)]
        public int maxCoreHealth = 1350;
        private int currentCoreHealth;

        private HealthSystem healthSystem;
        private _2_LevelCoreListener levelCoreListener;

        private _2_Subroutines subroutinesCommander;

        private void Awake() {
            if (GameObject.Find("CoreCanvas_Level-1") != null) {
                levelCoreListener = GameObject.Find("CoreCanvas_Level-1").
                    transform.Find("CoreHealthPanel").GetComponent<_2_LevelCoreListener>();
            }else if(GameObject.Find("CoreCanvas_Level-2") != null) {
                levelCoreListener = GameObject.Find("CoreCanvas_Level-2").
                    transform.Find("CoreHealthPanel").GetComponent<_2_LevelCoreListener>();
            }
                
                
        }

        #region Unity-Function
        private void Start() {
            subroutinesCommander = _2_Subroutines.Instance;
            subroutinesCommander.SetLevelCoreHandler(this);

            healthSystem = new HealthSystem(maxCoreHealth);
            levelCoreListener.SetHealthSystem(healthSystem);
            currentCoreHealth = maxCoreHealth;

            healthSystem.OnHealthEmpty += HealthSystem_OnHealthEmpty;
        }

        private void OnTriggerExit(Collider other) {
            if(other.gameObject.CompareTag("EnemyWeapon")) {
                var invader = other.GetComponent<_2_EnemyWeaponHitPoint>().MyTopperParent.GetComponent<IEnemy_Base>();
                invader.AttackLevelCore();

                if(healthSystem.GetHealthPercent() >= .75f) {
                    invader.UnderAttack(backShockPower);
                }else if(healthSystem.GetHealthPercent() >= .4f) {
                    invader.UnderAttack(backShockPower / 2.5f);
                }else if(healthSystem.GetHealthPercent() >= .25f) {
                    invader.UnderAttack(backShockPower / 4);
                }              
            }
        }
        #endregion

        #region Method
        public void UnderAttack(IEnemy_Base enemy, float damage) {
            healthSystem.Calculate_HealthPoint_Damage(damage);
            currentCoreHealth -= (int)damage;
        }
        public void UnderEnemyAttack( float damage)
        {
            healthSystem.Calculate_HealthPoint_Damage(damage);
            currentCoreHealth -= (int)damage;
        }

        private void DefendFailure_GameLoss() { //TODO
            //Animator.SetBool();
            //StartCoroutine(); 
        }
        #endregion

        private void HealthSystem_OnHealthEmpty(object sender, System.EventArgs e) {
            DefendFailure_GameLoss();
        }

    }

}
