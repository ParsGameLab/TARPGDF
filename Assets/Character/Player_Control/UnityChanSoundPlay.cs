using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanSoundPlay : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public void PlayFootStepSound()
    {
        audioSource.Play();
    }
}
