using UnityEngine;
using UnityEngine.UI;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class _2_StatBarListener_UnityChan : MonoBehaviour {

        private HealthSystem healthSystem;
        private ManaSystem manaSystem;

        private Image fillAmount_HealthPoint;
        private Image fillAmount_ManaPoint;

        private void Awake() {
            fillAmount_HealthPoint =
                transform.Find("StatBars").transform.Find("HealthBar").
                transform.Find("BarAnchor").transform.Find("Filler").GetComponent<Image>();
            fillAmount_ManaPoint =
               transform.Find("StatBars").transform.Find("ManaBar").transform.
               Find("BarAnchor").transform.Find("Filler").GetComponent<Image>();
        }


        public void SetHealthSystem(HealthSystem healthSystem) {
            this.healthSystem = healthSystem;
            this.healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        }
        public void SetHealthBarNormalizedSize() {
            float currentHealthBarNormalizedSize = fillAmount_HealthPoint.fillAmount;
            currentHealthBarNormalizedSize = healthSystem.GetHealthPercent();
            fillAmount_HealthPoint.fillAmount = currentHealthBarNormalizedSize;
        }

        public void SetManaSystem(ManaSystem manaSystem) {
            this.manaSystem = manaSystem;
            this.manaSystem.OnManaChanged += ManaSystem_OnManaChanged;
        }

        public void SetManaBarNormalizedSize() {
            float currentManaBarNormalizedSize = fillAmount_ManaPoint.fillAmount;
            currentManaBarNormalizedSize = manaSystem.GetManaPercent();
            fillAmount_ManaPoint.fillAmount = currentManaBarNormalizedSize;
        }

        private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e) {
            SetHealthBarNormalizedSize();
        }

        private void ManaSystem_OnManaChanged(object sender, System.EventArgs e) {
            SetManaBarNormalizedSize();
        }

    }

}
