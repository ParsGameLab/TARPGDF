using System.Collections.Generic;
using UnityEngine;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class _2_Subroutines : MonoBehaviour {

        private static _2_Subroutines instance;

        private _2_LevelCoreHandler levelCoreHandler;
        private _2_StatHandler_UnityChan statHandler_UnityChan;
        private Dictionary<string, int> dictionary_enemyBases = new Dictionary<string, int>();

        public static _2_Subroutines Instance => instance;

        public _2_LevelCoreHandler LevelCoreHandler => levelCoreHandler;

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

        public void SetLevelCoreHandler(_2_LevelCoreHandler levelCoreHandler) {
            this.levelCoreHandler = levelCoreHandler;
        }

        public void SetUnityChanStatHandler(_2_StatHandler_UnityChan statHandler_UnityChan) {
            this.statHandler_UnityChan = statHandler_UnityChan;
        }

        public void SetEnemyBases(IEnemy_Base enemyBase, string enemyName) {

            string newEnemyName = enemyName + '-' + (dictionary_enemyBases.Count + 1); 
            dictionary_enemyBases.Add(newEnemyName, dictionary_enemyBases.Count + 1);
            //Debug.Log(newEnemyName + "," + "id: " + dictionary_enemyBases[newEnemyName]);
            //Debug.Log(dictionary_enemyBases.Count);

        }

        
    }

}
