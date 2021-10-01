using System;
using System.Collections;
using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class _2_StatHandler_UnityChan : MonoBehaviour {

        #region Field 21-09-21
        private unitychanControl _unitychanControl;
        private SitchWeapon sitchWeapon;
        private WeaponController weaponController;
        private SkillAnimationEvent skillAnimationEvent;
        
        private int DeathAnimationHash = Animator.StringToHash("Magician_Die");
        private int DeathParameterHash = Animator.StringToHash("Death");

        private Animator myAnimator;
        private AnimatorStateInfo currentAnimatorStateInfo;
        #endregion

        #region Field
        private bool isDeath = false;
        public bool IsDeath => isDeath;

        

        private Vector3 myRespawnPosition;

        private _2_Subroutines subroutinesCommander;

        private HealthSystem healthSystem;
        private ManaSystem manaSystem;

        private _2_StatBarListener_UnityChan myStatBarListener;

        public int maxHealthPoint;
        private int currentHealthPoint;
        public int maxManaPoint;
        private int currentManaPoint;

        private float timer;
        #endregion

        private void Awake() {
            myStatBarListener = 
                GameObject.Find("Unity-Chan!Canvas").
                transform.Find("StatHUD_Unity-Chan!").GetComponent<_2_StatBarListener_UnityChan>();
            timer = 0f;

            #region 21-09-21
            myAnimator = GetComponent<Animator>();
            GetMyControlRelationalComponents();
            #endregion
        }

       
        private void Start() {
            subroutinesCommander = _2_Subroutines.Instance;
            subroutinesCommander.SetUnityChanStatHandler(this);

            healthSystem = new HealthSystem(maxHealthPoint);
            myStatBarListener.SetHealthSystem(healthSystem);
            currentHealthPoint = maxHealthPoint;

            manaSystem = new ManaSystem(maxManaPoint);
            myStatBarListener.SetManaSystem(manaSystem);
            currentManaPoint = maxManaPoint;

            
            #region 21-09-21
            healthSystem.OnHealthEmpty += HealthSystem_OnHealthEmpty;
            #endregion
        }
        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 5f)
            {
                manaSystem.RecoverManaPercent(10);
                timer = 0f;
            }
            
        }

        private void OnAnimatorMove() {
            
        }

        #region Method
        public void ExpendMana(float manaConsumedPoint) {
            if(currentManaPoint >= manaConsumedPoint) {
                manaSystem.CalculateManaPoint(manaConsumedPoint);
                currentManaPoint -= (int)manaConsumedPoint;
            } else {
                Debug.LogWarning("法力值不足，無法繼續施法。");
                return;
            }
        }
        public void TryUseMana(int usemana)
        {
            if (manaSystem.TrySpendManaAmount(usemana))
            {
                Debug.Log("canmana");
            }
            else
            {
                Debug.Log("notenought");
            }
        }
        public bool IsManaEnoughtoUse(int useMana)
        {
            if (manaSystem.IsManaEnough(useMana))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UnderAttack(IEnemy_Base enemy, float damagePoint) {
            healthSystem.Calculate_HealthPoint_Damage(damagePoint);
            currentHealthPoint -= (int)damagePoint;
        }
        public void UnderEnemyAttack(float damagePoint)
        {
            currentHealthPoint -= (int)damagePoint;
            healthSystem.Calculate_HealthPoint_Damage(damagePoint);
        }
        #endregion

        #region Method 21-09-21
        public void UpdateMyRespawnPosition(Vector3 newPosition) {
            myRespawnPosition = newPosition;
        }

        private void GetMyControlRelationalComponents() {
            _unitychanControl = GetComponent<unitychanControl>();
            sitchWeapon = GetComponent<SitchWeapon>();
            weaponController = GetComponent<WeaponController>();
        }

        private void EnableMyControlRelationalComponents() {
            //_unitychanControl.enabled = true;
            //weaponController.enabled = true;
            //sitchWeapon.enabled = true;
        }

        private void DisableMyControlRelationalComponents() {
            //_unitychanControl.enabled = false;
            //weaponController.enabled = false;
            //sitchWeapon.enabled = false;
        }

        private void ResetMyStat() {
            #region 21-09-21
            healthSystem.ResetHealthPoint(maxHealthPoint);
            currentHealthPoint = maxHealthPoint;
            #endregion

            #region 21-09-22
            manaSystem.ResetManaPoint(maxManaPoint);
            currentManaPoint = maxManaPoint;
            isDeath = false;
            #endregion
        }
        #endregion

        private void HealthSystem_OnHealthEmpty(object sender, EventArgs args) {
            DisableMyControlRelationalComponents();
            if(isDeath) { return; }
            isDeath = true;
            myAnimator.SetTrigger(DeathParameterHash);
            StartCoroutine(Respawn());
        }

        public IEnumerator Respawn() {
            while(currentAnimatorStateInfo.shortNameHash != DeathAnimationHash) {
                currentAnimatorStateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);                
                yield return null;
            }
            yield return new WaitForSeconds(2f);
            transform.position = myRespawnPosition;
            myAnimator.SetTrigger("Reaction");
            yield return new WaitForSeconds(0.5f);
            ResetMyStat();
            EnableMyControlRelationalComponents();
        }

    }

}