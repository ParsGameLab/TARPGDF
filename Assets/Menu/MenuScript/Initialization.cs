using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Initialization : MonoBehaviour
{
    public static Initialization instance;

    public void Awake()
    {
        instance = this;
       
        GameObject.Find("_2_Subroutines").GetComponent<MainMenu>().GameStart();

        //Debug.Log("123");
    }
   
}
