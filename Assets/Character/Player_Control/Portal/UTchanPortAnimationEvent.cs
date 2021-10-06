using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTchanPortAnimationEvent : MonoBehaviour
{
    public Transform targetdoor;
    public GameObject circleEffect;
    public GameObject targetPortEffect;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PreparBackCore()
    {
        circleEffect.SetActive(true);
        
    }
    public void BackCore()
    {
        transform.position = targetdoor.transform.position;
        var instance = Instantiate(targetPortEffect, targetdoor.position, targetdoor.rotation);
        Destroy(instance, 2f);
        circleEffect.SetActive(false);

    }
}
