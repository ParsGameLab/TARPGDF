using UnityEngine;


namespace iii_UMVR06_TPSDefenseGame_Subroutines_2.InventorySystem {

    [CreateAssetMenu(fileName = "a new Inventory", menuName = "Characters/Inventory")]
    public class InventoryObject : ScriptableObject {

        [SerializeField]
        private VoidEvent OnInventoryItemsUpdated = null;
        [SerializeField]
        private ItemSlot[] itemSlots = new ItemSlot[] { };

        public ItemContainer ItemContainer { get; } = new ItemContainer(15);

        public void OnEnable() {
            ItemContainer.OnItemsUpdated += OnInventoryItemsUpdated.Raise;
        }

        public void OnDisable() {
            ItemContainer.OnItemsUpdated -= OnInventoryItemsUpdated.Raise;
        }

        public void AddItem() {
            foreach(ItemSlot itemSlot in itemSlots) {
                ItemContainer.AddItem(itemSlot);
            }
        }

    }

}
