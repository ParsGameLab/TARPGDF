using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2.InventorySystem {
    public class _2_Subroutines_SubSystemManager : MonoBehaviour {

        [SerializeField]
        private InventoryObject inventoryObject;
        [SerializeField]
        private BagShortcuts bagShortcuts;

        public BagShortcuts BagShortcuts => bagShortcuts;

        public void Start() {
            inventoryObject.AddItem();
        }

    }

}
