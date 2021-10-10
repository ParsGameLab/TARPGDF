using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelLoad : MonoBehaviour
{
    public GameObject LoadingScreen;    
    public Slider slider;
    public Text progressText;

    
    public Sprite Menu_Sprite, S1_Sprite, S2_Sprite;

    //private string S1_Load = "Assets/Resources/S1_Load";

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsyncScene(sceneIndex));        
    }

    IEnumerator LoadAsyncScene(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        //Debug.Log("123");

        if (sceneIndex == 0)
            LoadingScreen.GetComponent<Image>().sprite = Menu_Sprite;
        else if (sceneIndex == 1)
            LoadingScreen.GetComponent<Image>().sprite = S1_Sprite;
        else if (sceneIndex == 2)
            LoadingScreen.GetComponent<Image>().sprite = S2_Sprite;


        LoadingScreen.SetActive(true);
        

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            float p = Mathf.Ceil(progress * 100f);

            slider.value = p;

            progressText.text = p + "%";            

            yield return null;
        }
    }

}
