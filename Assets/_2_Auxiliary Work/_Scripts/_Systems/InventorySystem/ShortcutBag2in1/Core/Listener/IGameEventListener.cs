namespace iii_UMVR06_TPSDefenseGame_Subroutines_2.InventorySystem {

    public interface IGameEventListener<T> {
        void OnEventRaised(T item);
    }

}