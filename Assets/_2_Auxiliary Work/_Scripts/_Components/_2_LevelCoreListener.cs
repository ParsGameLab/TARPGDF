using System;
using UnityEngine;
using UnityEngine.UI;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class _2_LevelCoreListener : MonoBehaviour {

        public Color fullTextColor;
        public Color lowTextColor;
        private Color currentTextColor;
        public Color fullHealthColor;
        public Color lowHealthColor;
        private Color currentFillerColor;

        private Image healthFill;
        private Text healthPercentText;
        private Text coreNameText;

        private HealthSystem healthSystem;

        private void Awake() {
            healthFill = transform.Find("Background").Find("Filler").GetComponent<Image>();
            coreNameText = transform.Find("CoreNameText").GetComponent<Text>(); 
            healthPercentText = transform.Find("HealthPercentText").GetComponent<Text>();
            currentFillerColor = healthFill.color;
            currentTextColor = coreNameText.color = healthPercentText.color;
        }


        public void SetHealthFillNormalizedSize() {
            float currentHealthFillNormalizedSize = healthFill.fillAmount;
            currentHealthFillNormalizedSize = healthSystem.GetHealthPercent();
            healthFill.fillAmount = currentHealthFillNormalizedSize;
            healthPercentText.text = (healthFill.fillAmount * 100).ToString("N1") + "%";

            currentFillerColor = Color.Lerp(lowHealthColor, fullHealthColor, healthFill.fillAmount);
            healthFill.color = currentFillerColor;
            currentTextColor = Color.Lerp(lowTextColor, fullHealthColor, healthFill.fillAmount);
            coreNameText.color = healthPercentText.color = currentTextColor;
            if(healthSystem.GetHealthPercent() <= 0) {
                coreNameText.color = healthPercentText.color = Color.gray;
            }
        }

        public void SetHealthSystem(HealthSystem healthSystem) {
            this.healthSystem = healthSystem;
            this.healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        }

        private void HealthSystem_OnHealthChanged(object sender, EventArgs e) {
            SetHealthFillNormalizedSize();
        }

    }

}
