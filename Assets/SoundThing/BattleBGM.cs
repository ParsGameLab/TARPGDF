using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBGM : MonoBehaviour
{
    private AudioSource audiosource;
    public static BattleBGM Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        audiosource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound()
    {
        audiosource.Play();
    }
    public IEnumerator WaitAndPlaySound(float f)
    {
        yield return new WaitForSeconds(f);
        PlaySound();
    }
    public  IEnumerator FadeMusic( float duration, float targetVolume)//StartCoroutine(FadeMusic(audioSource, 2, 0));
    {
        float currentTime = 0;
        float start = audiosource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audiosource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
    //public static IEnumerator FadeMusic(AudioSource audioSource, float duration, float targetVolume)//StartCoroutine(FadeMusic(audioSource, 2, 0));
    //{
    //    float currentTime = 0;
    //    float start = audioSource.volume;
    //    while (currentTime < duration)
    //    {
    //        currentTime += Time.deltaTime;
    //        audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
    //        yield return null;
    //    }
    //    yield break;
    //}
}
