using System.Collections.Generic;
using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class _2_Subroutines : MonoBehaviour {

        private static _2_Subroutines instance;

        private _2_InactiveEnemiesList list_InactiveEnemies;

        private _2_StatHandler_UnityChan statHandler_UnityChan;
        private Dictionary<IEnemy_Base, string> dictionary_enemyBases = new Dictionary<IEnemy_Base, string>();

        public static _2_Subroutines Instance => instance;

        public _2_InactiveEnemiesList List_InactiveEnemies => list_InactiveEnemies;
        public _2_StatHandler_UnityChan StatHandler_UnityChan => statHandler_UnityChan;

        private void Awake() {
            if(instance == null) {
                instance = this;
                DontDestroyOnLoad(instance);
                DontDestroyOnLoad(instance.gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        public void SetInactiveEnemiesList(_2_InactiveEnemiesList inactiveEnemiesList) {
            list_InactiveEnemies = inactiveEnemiesList;
        }

        public void SetUnityChanStatHandler(_2_StatHandler_UnityChan statHandler_UnityChan) {
            this.statHandler_UnityChan = statHandler_UnityChan;
        }

        public void SetEnemyBases(IEnemy_Base enemyBase, string enemyName) {
            dictionary_enemyBases.Add(enemyBase, enemyName);
        }

    }

}
