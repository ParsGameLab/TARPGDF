namespace iii_UMVR06_TPSDefenseGame_Subroutines_2.InventorySystem {
      
    public interface IItemContainer {

        ItemSlot AddItem(ItemSlot itemSlot);
    
        void Swap(int indexOne, int indexTwo);

        //bool HasItem(InventoryItem item);

        //int GetTotalQuantity(InventoryItem item);

        //void RemoveItem(ItemSlot itemSlot);

        //void RemoveAt(int slotIndex);

    }

}