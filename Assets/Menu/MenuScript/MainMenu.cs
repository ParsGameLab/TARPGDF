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
    public GameObject VictoryMenu;
    public GameObject FailMenu;


    public bool pauseMenuSwitch;
    public bool Canvas2_Switch;
    public bool Initialization_0;

    public int seance12=1;

    //private void Awake()
    //{
    //    DontDestroyOnLoad(this);
    //}
    private void Start()
    {
        seance12 = 1;
    }

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
        VictoryMenu.SetActive(false);
        FailMenu.SetActive(false);
        pauseMenuSwitch = false;        
    }

        public void MenuStart()
    {
        Cursor.visible = true;
        Time.timeScale = 1f;
        Canvas2_Switch = false;
        LoadingScreen.SetActive(false);
        Canvas2.SetActive(false);        
        PlayerUI.SetActive(false);
        FadeScreen.GetComponent<FadeInOut>().PlayFadeIn();
    }

    public void GameStart()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
        Canvas2_Switch = true;
        Canvas2.SetActive(true);
        Crosshair.SetActive(true);
        PlayerUI.SetActive(true);                      
        pauseMenu.SetActive(false);
        VictoryMenu.SetActive(false);
        FailMenu.SetActive(false);
        pauseMenuSwitch = false;
        GameObject.Find("Camera").GetComponent<TPScontt>().enabled = true;
        LoadingScreen.SetActive(false);
        FadeScreen.GetComponent<FadeInOut>().PlayFadeIn();
    }

    public void WIN()
    {
        Time.timeScale = 0f;
        Crosshair.SetActive(false);
        VictoryMenu.SetActive(true);
        pauseMenuSwitch = true;
        GameObject.Find("Camera").GetComponent<TPScontt>().enabled = false;
    }

    public void LOSE()
    {
        Time.timeScale = 0f;
        Crosshair.SetActive(false);
        FailMenu.SetActive(true);
        pauseMenuSwitch = true;
        GameObject.Find("Camera").GetComponent<TPScontt>().enabled = false;
    }


    //Update===========================
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuSwitch == false && Canvas2_Switch == true)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuSwitch == true && Canvas2_Switch == true)
        {
            ResumeGame();
        }
        else if (GameObject.Find("EnemySpot") != null &&seance12==1)
        {
            if(EnemySponManerger.Instance.IsS1Clear == true)
            {
                WIN();
            
                seance12 = 2;

            }
            
        }else if (GameObject.Find("EnemySpownPosS2") != null && seance12 == 2)
        {
            if (EnemySpawnManagerS2.Instance.IsS2Clear == true)
            {
                WIN();

            }
        }
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            WIN();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            LOSE();
        }



    }
}
