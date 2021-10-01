using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2.InventorySystem {

    public abstract class HotbarItem : ScriptableObject {

        [SerializeField] private new string name = "New Hotbar Item Name";
        [SerializeField] private Sprite icon = null;

        public string Name => name;

        public Sprite Icon => icon;

        public abstract string GetInfoDisplayText();

    }

}
