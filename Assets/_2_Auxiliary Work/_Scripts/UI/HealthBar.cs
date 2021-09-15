using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class HealthBar : MonoBehaviour {

        #region Field
        private Camera mainCamera;
        private HealthSystem reference_HealthSystem;

        private Transform barAnchor; 

        private float currentAngle;
        private float desiredAngle;
        #endregion

        #region Function
        private void Awake() {
            barAnchor = transform.Find("BarAnchor");
        }

        private void Start() {
            mainCamera = Camera.main;
        }

        private void LateUpdate() {
            SetHealthBarDirection_LookAt();
        }
        #endregion

        #region Method
        public void SetUp_HealthSystem(HealthSystem theHealthSystem) {
            reference_HealthSystem = theHealthSystem;
            reference_HealthSystem.OnHealthChanged += Ref_HealthSystem_OnHealthChanged;
        }

        public void SetUp_MainCamera(Camera theMainCamera) {
            mainCamera = theMainCamera;
        }

        public void SetSize() {
            barAnchor.localScale = new Vector3(reference_HealthSystem.GetHealthPercent(), barAnchor.localScale.y, barAnchor.localScale.z);
        }

        public void SetHealthBarDirection_LookAt() {
            transform.localRotation = Quaternion.LookRotation(mainCamera.transform.forward, Vector3.up);
        }
        #endregion

        #region Event
        private void Ref_HealthSystem_OnHealthChanged(object sender, System.EventArgs e) {
            SetSize();
        }
        #endregion

    }

}
