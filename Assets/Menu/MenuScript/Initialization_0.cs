using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Initialization_0 : MonoBehaviour
{
    public static Initialization_0 instance;
    public bool First_Time = true;

    public void Awake()
    {
        instance = this;        
       
        if (GameObject.Find("_2_Subroutines")!=null)
            GameObject.Find("_2_Subroutines").GetComponent<MainMenu>().MenuStart();
    }

   
}