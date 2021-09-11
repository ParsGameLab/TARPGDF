using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public abstract class IEnemy_Base : MonoBehaviour {

        #region Field
        protected Camera mainCamera;
        protected HealthBar myHealthBar;
        protected HealthSystem reference_HealthSystem;

        protected float currentHealthPoint;
        [SerializeField] 
        protected float maxHealthPoint;

        //float middlePointX_mainCameraEyes;
        //float middlePointY_mainCameraEyes;
        //Vector3 middlePoint_mainCameraEyes;
        #endregion

        #region Function
        protected virtual void Awake() {
            myHealthBar = transform.GetChild(0).transform.Find("HealthBar").GetComponent<HealthBar>();
        }

        protected virtual void Start() {
            mainCamera = Camera.main;
            myHealthBar.SetUp_MainCamera(mainCamera);
            //middlePointX_mainCameraEyes = mainCamera.pixelWidth / 2;
            //middlePointY_mainCameraEyes = mainCamera.pixelHeight / 2;
            //middlePoint_mainCameraEyes = new Vector3(middlePointX_mainCameraEyes, middlePointY_mainCameraEyes, 0);

            reference_HealthSystem = new HealthSystem(maxHealthPoint);

            currentHealthPoint = maxHealthPoint;

            myHealthBar.SetUp_HealthSystem(reference_HealthSystem);
            myHealthBar.SetSize();            
        }

        public virtual void Update() {
            
            HasBeAimedAlready();

            /*******************************************
             * 僅在 Project Review 時使用。
             ******************************************/
            if(isAWoodendummy && eventCount == 0) {
                reference_HealthSystem.OnHealthCharging += Reference_HealthSystem_OnHealthCharging;
                eventCount++;
            } else if(!isAWoodendummy && eventCount > 0) {
                reference_HealthSystem.OnHealthCharging -= Reference_HealthSystem_OnHealthCharging;
                eventCount--;
            }

            OnProjectReview();
        }
        #endregion

        #region Method
        //public virtual void UnderAttack(float damagePoint) {
        //    currentHealthPoint -= (int)damagePoint;
        //    reference_HealthSystem.Calculate_HealthPoint_Damage(damagePoint);
        //}

        private void HasBeAimedAlready() {       
            Ray middlePointRay_mainCamera = mainCamera.ScreenPointToRay(CalculateTheCrossHairPosition());
            RaycastHit hitInfo;
            if(Physics.Raycast(middlePointRay_mainCamera, out hitInfo, 750f)) {
                if(hitInfo.transform.GetComponent<Collider>().CompareTag("Enemy")) {
                    GameObject beHitterHealthBar = hitInfo.transform.GetChild(0).transform.Find("HealthBar").gameObject;
                    beHitterHealthBar.SetActive(true);
                } else {
                    myHealthBar.gameObject.SetActive(false);
                }
            } else {
                myHealthBar.gameObject.SetActive(false);
            }
        }

        private Vector3 CalculateTheCrossHairPosition() {
            float middlePointX_mainCameraEyes = mainCamera.pixelWidth / 2;
            float middlePointY_mainCameraEyes = mainCamera.pixelHeight / 2;
            Vector3 middlePoint_mainCameraEyes = new Vector3(middlePointX_mainCameraEyes, middlePointY_mainCameraEyes, 0);
            return middlePoint_mainCameraEyes;
        }
        #endregion

        /*******************************************
         * 僅在 Project Review 時使用。
         ******************************************/
        #region Field
        public bool isAWoodendummy = true;
        private int eventCount = 0;
        private bool isInvincible = false;

        private float delayTimer = 0;
        #endregion

        #region Method
        private void OnProjectReview() {
            if(isInvincible) {
                delayTimer = 0;
                Invoke("BeginCharge", .15f);
            } else {
                delayTimer += Time.deltaTime;
                if(isAWoodendummy && delayTimer > .85f) {
                    Invoke("BeginRestore", .35f);
                }
            }
        }
        public virtual void UnderAttack(float damagePoint) {
            if(isInvincible) {
                return;
            }

            delayTimer = 0;
            myHealthBar.SetColor(Color.red);

            currentHealthPoint -= (int)damagePoint;
            reference_HealthSystem.Calculate_HealthPoint_Damage(damagePoint, isAWoodendummy);
        }
        private void BeginCharge() {
            Charge_Broken(Time.deltaTime * 265f);            
        }

        private void BeginRestore() {
            Restore_Standby(Time.deltaTime * 235f);
        }

        protected virtual void Charge_Broken(float chargePoint) {
            currentHealthPoint += (int)chargePoint;
            if(currentHealthPoint % 1f == 0) {
                myHealthBar.SetColor(Color.green);
            }

            reference_HealthSystem.Calculate_HealthPoint_Heal(chargePoint);

            if(currentHealthPoint >= maxHealthPoint) {
                currentHealthPoint = maxHealthPoint;
                myHealthBar.SetColor(Color.red);
                isInvincible = false;
            }
        }

        protected virtual void Restore_Standby(float restorPoint) {
            currentHealthPoint += (int)restorPoint;

            reference_HealthSystem.Calculate_HealthPoint_Heal(restorPoint);

            if(currentHealthPoint >= maxHealthPoint) {
                currentHealthPoint = maxHealthPoint;
            }
        }
        #endregion

        #region Event
        private void Reference_HealthSystem_OnHealthCharging(object sender, System.EventArgs e) {
            isInvincible = true;
        }
        #endregion

    }

}
