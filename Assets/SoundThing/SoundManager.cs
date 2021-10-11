using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private AudioSource audio_source;
    public enum Sound
    {
        PlayerAtk,
        Win,
        Cannon,
        Expolsion,
        Respown,
        HitBigMob,
        HitBody,
        HitHead,
        Skill,
        fire,
        lastwave,
        Horn,
        NeedleTrap,
        KnifeTrapNor,
        KnifeTrapAtk,
        MagicSlash,
        MagicBall,
        earthimpact,
        fireimpact,
        UltStrike,
    }
    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();
        foreach(Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }
    public void GoWithSound(Sound sound, Vector3 pos,GameObject sound_play_object)
    {
        GameObject s = new GameObject();
        s.transform.parent = sound_play_object.transform;
        s.transform.position = pos;//3D音效的位置

        audio_source = s.AddComponent<AudioSource>();

        AudioClip clip = soundAudioClipDictionary[sound];
        audio_source.clip = clip;
        audio_source.PlayOneShot(clip);
        //audio_source.playOnAwake = true;
        //audio_source.spatialBlend = 1.0f; // 3D音效
    }
    public AudioClip GetSoundClip(Sound sound)
    {
        return soundAudioClipDictionary[sound];
    }
    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound]);
    }
}
