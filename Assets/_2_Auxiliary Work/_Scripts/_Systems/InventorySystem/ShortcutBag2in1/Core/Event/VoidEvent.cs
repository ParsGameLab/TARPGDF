using UnityEngine;

/****************
 * UTF-8
 ***************/
namespace iii_UMVR06_TPSDefenseGame_Subroutines_2.InventorySystem {

    [CreateAssetMenu(fileName = "New Void Event", menuName = "Game Events/Void Event")]
    public class VoidEvent : BaseGameEvent<Void> {

        public void Raise() => Raise(new Void());

    }

}