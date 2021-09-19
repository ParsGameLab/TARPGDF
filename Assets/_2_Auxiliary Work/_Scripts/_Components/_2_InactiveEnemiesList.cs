using System.Collections.Generic;
using UnityEngine;

/*****************
 * UTF-8
 * **************/
namespace iii_UMVR06_TPSDefenseGame_Subroutines_2 {

    public class _2_InactiveEnemiesList : MonoBehaviour {

        [SerializeField] 
        private static List<IEnemy_Base> list_InactiveEnemies = new List<IEnemy_Base>();

        public Transform InactiveEnemiesTransform => transform;

        private void Start() {
            _2_Subroutines.Instance.SetInactiveEnemiesList(this);   
        }

        /// <summary>
        /// 【個別重設】
        /// 重設enemy所應附屬在的物件與位置。
        /// </summary>
        /// <param name="whichTransform">要重新附屬在哪個物件底下？</param>
        /// <param name="index">清單中，「第幾個」索引編號的enemy？（從"[0]"開始）</param>
        public static void ReTransformAndPosition_Single(Transform whichTransform, int index) {
            list_InactiveEnemies[index].transform.parent = whichTransform;
            list_InactiveEnemies[index].transform.localPosition = Vector3.zero;
            list_InactiveEnemies.Remove(list_InactiveEnemies[index]);

            Debug.Log("重設完畢。");

            if(list_InactiveEnemies.Count > 0) {
                Debug.Log("清單尚有： " + list_InactiveEnemies.Count + " 個enemy物件");
            } else {
                Debug.Log("清單已無enemy物件。");
            }
        }
        /// <summary>
        /// 【複數重設】
        /// 重設enemies所應附屬在的物件與位置。
        /// </summary>
        /// <param name="whichTransform">要重新附屬在哪個物件底下？</param>
        /// <param name="firstIndex">要將清單中，「第幾個」索引編號的enemy，作為循環迴圈的開頭？（從"[0]"開始）</param>
        /// <param name="lastIndex">要將清單中，「第幾個」索引編號的enemy，作為循環迴圈的結尾？（從"[0]"開始）</param>
        public static void ReTransformAndPosition_RangeIndex(Transform whichTransform, int firstIndex, int lastIndex) {
            for(int i = firstIndex; i < lastIndex; i++) {
                list_InactiveEnemies[i].transform.parent = whichTransform;
                list_InactiveEnemies[i].transform.localPosition = Vector3.zero;
            }     
            for(int i = lastIndex - 1; i >= firstIndex; i--) {
                list_InactiveEnemies.RemoveAt(i);
            }

            Debug.Log("重設完畢。");

            if(list_InactiveEnemies.Count > 0) {
                Debug.Log("清單尚有： " + list_InactiveEnemies.Count + " 個enemy物件");
            } else {
                Debug.Log("清單已無enemy物件。");
            }
        }
        /// <summary>
        /// 【複數重設－隨機附屬】
        /// 重設enemies所應附屬在的物件與位置。
        /// </summary>
        /// <param name="whichTransforms">要重新附屬在哪幾個物件底下？</param>
        /// <param name="firstIndex">要將清單中，「第幾個個」索引編號的enemy，作為循環迴圈的開頭？（從"[0]"開始）</param>
        /// <param name="lastIndex">要將清單中，「第幾個」索引編號的enemy，作為循環迴圈的結尾？（從"[0]"開始）</param>
        public static void ReTransformAndPosition_RandomTransform(Transform[] whichTransforms, int firstIndex, int lastIndex) {
            for(int i = firstIndex; i < lastIndex; i++) {
                list_InactiveEnemies[i].transform.parent = whichTransforms[Random.Range(0, whichTransforms.Length)];
                list_InactiveEnemies[i].transform.localPosition = Vector3.zero;
            }
            for(int i = lastIndex - 1; i >= firstIndex; i--) {
                list_InactiveEnemies.RemoveAt(i);
            }

            Debug.Log("重設完畢。");

            if(list_InactiveEnemies.Count > 0) {
                Debug.Log("清單尚有： " + list_InactiveEnemies.Count + " 個enemy物件");
            } else {
                Debug.Log("清單已無enemy物件。");
            }
        }

        public void AddToInactiveEnemiesList(IEnemy_Base enemy, bool isInactive) {
            if(isInactive != false) {
                Debug.LogWarning("The enemy which want to add to inactive enemies list is still active.");
                return;
            }
            list_InactiveEnemies.Add(enemy);
        }
    }

}