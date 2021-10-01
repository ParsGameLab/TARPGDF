using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{ 
    public void PlayFadeIn()
    {
        Debug.Log("123");
        GetComponent<Animation>().Play("FadeIn");
    }

    public void PlayFadeOut()
    {
        GetComponent<Animation>().Play("FadeOut");
    }
}
