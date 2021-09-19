using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public abstract class IEnemy_Base : MonoBehaviour {

        #region Field
        protected _2_Subroutines subroutinesCommander;
        protected _2_StatHandler_UnityChan myEnemy_1;

        [SerializeField] protected string myName;

        private bool isInActive = true;

        [SerializeField][Min(1)]
        protected float attackPower;

        private bool isDeath = false;

        protected EnemyAnimations myAnimations;

        protected Camera mainCamera;
        protected HealthBar myHealthBar;
        protected HealthSystem reference_HealthSystem;

        [SerializeField][Min(1)]
        protected float maxHealthPoint;
        private float currentHealthPoint;
        #endregion

        #region Function
        protected virtual void Awake() {
            myHealthBar = transform.GetChild(0).transform.Find("HealthBar").GetComponent<HealthBar>();
            myAnimations = new EnemyAnimations(this, transform.GetChild(0).GetComponent<Animator>());
            subroutinesCommander = _2_Subroutines.Instance;
        }

        protected virtual void Start() {
            _2_Subroutines.Instance.SetEnemyBases(this, myName);

            mainCamera = Camera.main;
            myHealthBar.SetUp_MainCamera(mainCamera);

            
            reference_HealthSystem = new HealthSystem(maxHealthPoint);
            reference_HealthSystem.OnHealthEmpty += Reference_HealthSystem_OnHealthEmpty;

            currentHealthPoint = maxHealthPoint;

            myHealthBar.SetUp_HealthSystem(reference_HealthSystem);
            myHealthBar.SetSize();

        }

        public virtual void Update() {        
            HasBeAimedAlready_Type2();
        }
        #endregion

        #region Method
        public virtual void Attack() {
            subroutinesCommander.StatHandler_UnityChan.UnderAttack(this, attackPower);
        }
        public virtual void UnderAttack(float damagePoint) {
            currentHealthPoint -= (int)damagePoint;
            reference_HealthSystem.Calculate_HealthPoint_Damage(damagePoint);
        }
        private void SetMyselfUnActive() {
            transform.localPosition = Vector3.zero;

            gameObject.SetActive(false);
        }

        public void SetUnityChanStatHandler(_2_StatHandler_UnityChan statHandler_UnityChan) {
            myEnemy_1 = statHandler_UnityChan;
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

        #region Event
        private void Reference_HealthSystem_OnHealthEmpty(object sender, System.EventArgs e) {
            if(isDeath) { return; }
            myAnimations.OnDeath();
            isDeath = true;

            Invoke("SetMyselfUnActive", 2.5f);
            isInActive = false;

            _2_Subroutines.Instance.List_InactiveEnemies.AddToInactiveEnemiesList(this, isInActive);
            transform.parent = _2_Subroutines.Instance.List_InactiveEnemies.InactiveEnemiesTransform;
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
