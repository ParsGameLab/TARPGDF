using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleeding : MonoBehaviour
{
    public GameObject bloodImage;
    private Animator anime;
    private void Start()
    {
        anime = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!anime.GetCurrentAnimatorStateInfo(0).IsName("Magician_Die"))
        {
            bloodImage.SetActive(false);
        }
        else
        {
            bloodImage.SetActive(true);
        }
    }
}
