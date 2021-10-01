using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2.InventorySystem {

    public class InventorySlot : ItemSlotUI { //, IDropHandler

        //[SerializeField] private Inventory inventory = null;
        [SerializeField] private InventoryObject inventoryObject = null;

        public override HotbarItem SlotItem {
            get { return ItemSlot.item; }
            set { }
        }

        public ItemSlot ItemSlot => inventoryObject.ItemContainer.GetSlotByIndex(SlotIndex);

        public override void UpdateSlotUI() {
            if(ItemSlot.item == null) {
                EnableSlotUI(false);
                return;
            }

            EnableSlotUI(true);
            itemIconImage.sprite = ItemSlot.item.Icon;
        }
        protected override void EnableSlotUI(bool enable) {
            base.EnableSlotUI(enable);
        }

        //public override void OnDrop(PointerEventData eventData) {
        //    ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();
        //    if(itemDragHandler == null) { return; }
        //}

    }

}
