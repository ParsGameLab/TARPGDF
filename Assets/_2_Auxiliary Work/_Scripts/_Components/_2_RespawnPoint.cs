using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class _2_RespawnPoint : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {
            _2_StatHandler_UnityChan statHandler_UnityChan = other.GetComponent<_2_StatHandler_UnityChan>();
            if(statHandler_UnityChan == null) { return; }
            statHandler_UnityChan.UpdateMyRespawnPosition(transform.position);
        }
    }

}
