using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UTchanPortAnimationEvent : MonoBehaviour
{
    public Transform targetdoor;
    public GameObject circleEffect;
    public GameObject targetPortEffect;
    private CharacterController CCC;
    
    void Start()
    {
        CCC = transform.GetComponent<CharacterController>();
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
        CCC.enabled = false;
        SoundManager.Instance.PlaySound(SoundManager.Sound.TelePortF);
        transform.position = targetdoor.transform.position;
        //SoundManager.Instance.PlaySound(SoundManager.Sound.TelePortGO);
        CCC.enabled = true;
        var instance = Instantiate(targetPortEffect, targetdoor.position, targetdoor.rotation);
        Destroy(instance, 2f);
        circleEffect.SetActive(false);

        this.gameObject.GetComponentInChildren<UnityChan_ShaderContral>().enabled = true;
        this.gameObject.GetComponentInChildren<UnityChan_ShaderContral>().Portal_SW();
    }
}
