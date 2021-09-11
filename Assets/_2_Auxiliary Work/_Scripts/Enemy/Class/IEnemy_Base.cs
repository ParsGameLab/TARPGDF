using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public abstract class IEnemy_Base : MonoBehaviour {

        #region Field
        public GameObject target;
        protected Quaternion myOriginRotation;

        [SerializeField]
        protected float confuseTime = 2.5f;
        private float confuseTimer;

        [SerializeField]
        protected float detectDistance;
        [SerializeField]
        protected float acceptableMaxHeightOffset;
        [SerializeField]
        protected float myVision;

        public LayerMask detectLayerMask;

        private bool isDeath = false;

        protected EnemyAnimations myAnimations;

        protected Camera mainCamera;
        protected HealthBar myHealthBar;
        protected HealthSystem reference_HealthSystem;

        [SerializeField] 
        protected float maxHealthPoint;
        private float currentHealthPoint;

        protected IEnemyBehaviourSystem_Base reference_CurrentState;
        public readonly IEnemyBehaviourSystem_Base detectionState = new DetectionState();
        #endregion

        #region Property
        public Quaternion MyOriginRotation => myOriginRotation;
        public float ConfuseTime => confuseTime;
        public float DetectDistance => detectDistance;
        public float AcceptalbeMaxHeightOffset => acceptableMaxHeightOffset;
        public float MyVision => myVision;
        public LayerMask DetectLayerMask => detectLayerMask;

        public GameObject Target {
            get { return target; }
            set { target = value; }
        }

        public float ConfuseTimer {
            get {
                return confuseTimer;
            }
            set {
                confuseTimer = value;
            }
        }
        #endregion

        #region Function
        protected virtual void OnEnable() {
            reference_CurrentState = detectionState;
            reference_CurrentState.EnterState(this);
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

            myOriginRotation = transform.rotation;
        }

        public virtual void Update() {        
            HasBeAimedAlready_Type2();
            reference_CurrentState.Update();
        }

        protected virtual void OnDrawGizmosSelected() {
            Gizmos.color = new Color(Color.green.r, Color.green.g, Color.green.b, .55f);
            Gizmos.DrawSphere(transform.position, detectDistance);

            Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, .55f);
            Gizmos.DrawLine(
                transform.position,
                transform.position + Vector3.up * acceptableMaxHeightOffset);
            Gizmos.DrawLine(
                transform.position,
                transform.position - Vector3.up * acceptableMaxHeightOffset);

            UnityEditor.Handles.color = new Color(Color.red.r, Color.red.g, Color.red.b, .6f);
            UnityEditor.Handles.DrawSolidArc(
                transform.position,
                Vector3.up, transform.forward,
                myVision,
                detectDistance);

            UnityEditor.Handles.DrawSolidArc(
                transform.position,
                -Vector3.up, transform.forward,
                myVision,
                detectDistance);
        }
        #endregion

        #region Method
        public void TransitionToState(IEnemyBehaviourSystem_Base theTargetState) {
            reference_CurrentState = theTargetState;
            theTargetState.EnterState(this);
        }
        
        public void LookTarget() {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(vectorToTarget);
        }

        public void LookOriginDirection() {
             transform.rotation = Quaternion.Lerp(transform.rotation, myOriginRotation, .005f);
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

        public virtual void UnderAttack(float damagePoint) {
            currentHealthPoint -= (int)damagePoint;
            reference_HealthSystem.Calculate_HealthPoint_Damage(damagePoint);
        }
        #endregion

        #region Event
        private void Reference_HealthSystem_OnHealthEmpty(object sender, System.EventArgs e) {
            if(isDeath) { return; }
            myAnimations.OnDeath();
            isDeath = true;
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
