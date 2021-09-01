using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class HealthBar : MonoBehaviour {

        #region Field
        private HealthSystem ref_HealthSystem;

        private Camera mainCamera;

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
            SetHealthBarDirection();
        }
        #endregion

        #region Method
        private void Ref_HealthSystem_OnHealthChanged(object sender, System.EventArgs e) {
            SetSize();
        }

        public void SetUp_HealthSystem(HealthSystem theHealthSystem) {
            ref_HealthSystem = theHealthSystem;
            ref_HealthSystem.OnHealthChanged += Ref_HealthSystem_OnHealthChanged;
        }

        public void SetSize() {
            barAnchor.localScale = new Vector3(ref_HealthSystem.GetHealthPercent(), 1f, 0);
        }

        public void SetHealthBarDirection() {
            currentAngle = transform.localEulerAngles.y;
            desiredAngle = mainCamera.transform.localEulerAngles.y;
            currentAngle = Mathf.LerpAngle(currentAngle, desiredAngle, 1f);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        }
        #endregion
    }

}
