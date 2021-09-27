using System;
using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public abstract class IEnemy_Base : MonoBehaviour {

        #region event & Property - For Outside
        public event EventHandler OnEnemyDeath;
        public bool IsDeath => isDeath;
        private bool canHit;
        #endregion

        #region Field
        [SerializeField][Min(1)]
        protected float attackPower;

        private bool isDeath = false;
        protected EnemyAnimations myAnimations;

        protected Camera mainCamera;
        protected HealthBar myHealthBar;
        protected HealthSystem reference_HealthSystem;

        [SerializeField][Min(1)]
        protected float maxHealthPoint;
        public float currentHealthPoint;

        protected string[] myName;
        #endregion

        #region Function
        protected virtual void OnEnable() {
            //Debug.Log("OnEnable.");
        }

        protected virtual void OnDisable() {
            //Debug.Log("OnDisable.");
        }

        protected virtual void Awake() {
            myHealthBar = transform.GetChild(0).transform.Find("HealthBar").GetComponent<HealthBar>();
            myAnimations = new EnemyAnimations(this, transform.GetChild(0).GetComponent<Animator>());
        }

        protected virtual void Start() {
            mainCamera = Camera.main;
            myHealthBar.SetUp_MainCamera(mainCamera);

            reference_HealthSystem = new HealthSystem(maxHealthPoint);
            reference_HealthSystem.OnHealthEmpty += Reference_HealthSystem_OnHealthEmpty;

            currentHealthPoint = maxHealthPoint;

            myHealthBar.SetUp_HealthSystem(reference_HealthSystem);
            myHealthBar.SetSize();

            myName = gameObject.name.Split('(');
            _2_Subroutines.Instance.SetEnemyBases(this, myName[0]);

            canHit = true;
        }

        public virtual void Update() {        
            HasBeAimedAlready_Type2();
        }
        #endregion

        #region Method - For Outside
        public void ResetEnemyStat() {
            reference_HealthSystem.ResetHealthPoint(maxHealthPoint);
            currentHealthPoint = maxHealthPoint;
            isDeath = false;
        }

        public virtual void UnderAttack(float damagePoint) {
            currentHealthPoint -= (int)damagePoint;
            if (isDeath == false && canHit && damagePoint > 20f)
            {
                myAnimations.GetHit();

            }
            reference_HealthSystem.Calculate_HealthPoint_Damage(damagePoint);
        }
        #endregion

        #region event
        private void Reference_HealthSystem_OnHealthEmpty(object sender, System.EventArgs e) {
            if(isDeath) { return; }
            myAnimations.OnDeath();
            OnEnemyDeath?.Invoke(this, EventArgs.Empty);
            isDeath = true;
        }
        #endregion 

        #region Method
        public virtual void Attack() {
            _2_Subroutines.Instance.StatHandler_UnityChan.UnderAttack(this, attackPower);
        }

        private void HasBeAimedAlready_Type2() {
            Ray middlePointRay_mainCamera = mainCamera.ScreenPointToRay(Utils.CalculateTheCrossHairPosition(mainCamera));
            RaycastHit[] results = new RaycastHit[15];
            GameObject[] beHitterHealthBars = new GameObject[15];
            int index = 0;
            int numbers = Physics.RaycastNonAlloc(middlePointRay_mainCamera, results, 750f);
            for(int i = 0; i < numbers; i++) {
                if(results[i].transform.GetComponent<Collider>().CompareTag("Enemy") && !results[i].transform.GetComponent<IEnemy_Base>().isDeath) {
                    beHitterHealthBars[index] = results[i].transform.GetChild(0).transform.Find("HealthBar").gameObject;
                    index++;
                }
            }

            myHealthBar.gameObject.SetActive(false);

            if(beHitterHealthBars[0] != null) {
                beHitterHealthBars[0].SetActive(true);
            }
        }
        #endregion

    }

    public class EnemyAnimations {

        private Animator animator;

        public EnemyAnimations(IEnemy_Base checkWhoAreYou, Animator theAnimator) {
            animator = theAnimator;
        }

        public void OnDeath() {
            animator.SetTrigger("death");
        }
        public void GetHit()
        {
            animator.SetTrigger("GetHit");
        }

    }

    public static class Utils {

        public static Vector3 CalculateTheCrossHairPosition(Camera mainCamera) {
            float middlePointX_mainCameraEyes = mainCamera.pixelWidth / 2;
            float middlePointY_mainCameraEyes = mainCamera.pixelHeight / 2;
            Vector3 middlePoint_mainCameraEyes = new Vector3(middlePointX_mainCameraEyes, middlePointY_mainCameraEyes, 0);
            return middlePoint_mainCameraEyes;
        }

    }

}
