using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Initialization_0 : MonoBehaviour
{
    public static Initialization_0 instance;

    public void Awake()
    {
        GameObject.Find("FadeScreen").GetComponent<FadeInOut>().PlayFadeIn();

        instance = this;
       
        GameObject.Find("_2_Subroutines").GetComponent<MainMenu>().MenuStart();
    }

}
