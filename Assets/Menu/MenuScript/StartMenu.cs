using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject FadeScreen;
    public GameObject Options_M;
    public GameObject Start_M;

    public void Awake()
    {
        FadeScreen.GetComponent<FadeInOut>().PlayFadeIn();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Update===========================
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Options_M.SetActive(false);
            Start_M.SetActive(true);
        }
    }

}
