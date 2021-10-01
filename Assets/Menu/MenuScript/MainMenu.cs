using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject Crosshair;
    public GameObject PlayerUI;
    public GameObject LoadingScreen;
    public GameObject Canvas2;
    public GameObject FadeScreen;

    public bool pauseMenuSwitch;
    
    //private void Awake()
    //{
    //    DontDestroyOnLoad(this);
    //}

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {        
        Time.timeScale = 0f;
        Crosshair.SetActive(false);
        pauseMenu.SetActive(true);
        pauseMenuSwitch = true;
        GameObject.Find("Camera").GetComponent<TPScontt>().enabled = false;
    }        

    public void ResumeGame()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        Crosshair.SetActive(true);
        pauseMenu.SetActive(false);
        pauseMenuSwitch = false;
        GameObject.Find("Camera").GetComponent<TPScontt>().enabled = true;
    }

    public void BackToMenu()
    {        
        Crosshair.SetActive(false);
        pauseMenu.SetActive(false);
        pauseMenuSwitch = false;        
    }

        public void MenuStart()
    {
        Cursor.visible = true;
        Time.timeScale = 1f;
        LoadingScreen.SetActive(false);
        Canvas2.SetActive(false);        
        PlayerUI.SetActive(false);
        FadeScreen.GetComponent<FadeInOut>().PlayFadeIn();
    }

    public void GameStart()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        Canvas2.SetActive(true);
        Crosshair.SetActive(true);
        PlayerUI.SetActive(true);                      
        pauseMenu.SetActive(false);
        pauseMenuSwitch = false;
        GameObject.Find("Camera").GetComponent<TPScontt>().enabled = true;
        LoadingScreen.SetActive(false);
        FadeScreen.GetComponent<FadeInOut>().PlayFadeIn();
    }


    //Update===========================
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuSwitch == false)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuSwitch == true)
        {            
            ResumeGame();
        }
        
    }
}
