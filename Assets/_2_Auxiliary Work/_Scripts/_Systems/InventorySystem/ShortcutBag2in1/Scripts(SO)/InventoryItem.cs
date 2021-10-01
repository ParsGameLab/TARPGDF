using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2.InventorySystem {

    public abstract class InventoryItem : HotbarItem {

        [SerializeField][Min(1)] 
        private int maxStack = 1;

        public int MaxStack => maxStack;

    }

}
