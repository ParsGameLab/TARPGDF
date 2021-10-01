using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace iii_UMVR06_TPSDefenseGame_Subroutines_2.InventorySystem {

    public class BagShortcuts : MonoBehaviour {

        //Dictionary<GameObject, KeyCode> objectToTogglesDictionary = new Dictionary<GameObject, KeyCode>();

        private int currentSelectSlotIndex = 0;
        public int CurrentSelectSlot => currentSelectSlotIndex;

        public ToggleShortcutWithKeyPress[] itemSlots; 

        public GameObject[] ToToggleObjects;
        public KeyCode[] itemSlotKeyCodes;

        private void Start() {
            for(int i = 0; i < itemSlots.Length; i++) {      
                ToToggleObjects[i] = itemSlots[i].objectToToggle;
                itemSlotKeyCodes[i] = itemSlots[i].keyCode;
            }
            ToToggleObjects[0].GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
            ToToggleObjects[0].GetComponent<RectTransform>().pivot = new Vector2(1.35f, -1.35f);
        }

        private void Update() {
            SelectHotbarSlot();
            //Debug.Log(currentSelectSlotIndex);
        }

        public int SelectHotbarSlot() {
            for(int i = 0; i < itemSlotKeyCodes.Length; i++) {
                if(Input.GetKeyDown(itemSlotKeyCodes[i])) {
                    foreach(var ToToggleObject in ToToggleObjects) {
                        ToToggleObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                        ToToggleObject.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
                    }
                    ToToggleObjects[i].GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1);
                    ToToggleObjects[i].GetComponent<RectTransform>().pivot = new Vector2(1.35f, -1.35f);

                    currentSelectSlotIndex = i;

                    return i - 1;
                }                
            }
            return currentSelectSlotIndex - 1;
        }
    }

}
