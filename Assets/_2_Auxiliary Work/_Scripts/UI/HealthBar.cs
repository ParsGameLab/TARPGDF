using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class HealthBar : MonoBehaviour {

        #region Field
        private HealthSystem reference_HealthSystem;

        private Camera mainCamera;

        private Transform barAnchor;
        private Color barSpriteColor;

        private float currentAngle;
        private float desiredAngle;
        #endregion

        #region Property
        public Color BarSpriteColor {
            get { return barSpriteColor; }
        }
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
        public void Show() {
            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }

        public void SetUp_HealthSystem(HealthSystem theHealthSystem) {
            reference_HealthSystem = theHealthSystem;
            reference_HealthSystem.OnHealthChanged += Ref_HealthSystem_OnHealthChanged;
        }

        public void SetSize() {
            barAnchor.localScale = new Vector3(reference_HealthSystem.GetHealthPercent(), barAnchor.localScale.y, barAnchor.localScale.z);
        }

        public void SetColor(Color theColor) {
            barAnchor.Find("BarSprite").GetComponent<SpriteRenderer>().color = theColor;
        }

        public void SetHealthBarDirection() {
            currentAngle = transform.localEulerAngles.y;
            desiredAngle = mainCamera.transform.localEulerAngles.y;
            currentAngle = Mathf.LerpAngle(currentAngle, desiredAngle, 1f);
            transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        }
        #endregion

        #region Event
        private void Ref_HealthSystem_OnHealthChanged(object sender, System.EventArgs e) {
            SetSize();
        }
        #endregion
    }

}
