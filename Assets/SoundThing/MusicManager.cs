using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audiosource;
    public static MusicManager Instance { get; private set; }
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
    public void StopSound()
    {
        audiosource.Stop();
    }
    public IEnumerator WaitAndPlaySound(float f)
    {
        yield return new WaitForSeconds(f);
        PlaySound();
    }
    void OnDisable()
    {
        //StartCoroutine(FadeMusic(audioSource, 2, 0));
        Debug.Log("當對象變為不可用或是不被調用狀態時此函數被調用");
    }
    //public static IEnumerator FadeMusic(AudioSource audioSource, float duration, float targetVolume)
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
