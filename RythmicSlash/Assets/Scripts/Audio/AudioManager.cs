using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    [SerializeField] AudioClip[] soundEffects;
    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();

    [SerializeField] private Sounds[] sounds;

    public void PlaySound(string soundName)
    {
        foreach (Sounds sound in sounds)
        {
            if (sound.clipName == soundName)
            {
                PlaySound(sound.audioClip);
            }
        }

        AudioSource source = CreateAudioSource();
    }

    public void PlaySound(AudioClip clip)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }

        AudioSource source = CreateAudioSource();

        source.clip = clip;
        source.Play();
    }

    public void PlaySound(int soundIndex)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = soundEffects[soundIndex];
                audioSource.Play();

                return;
            }
        }

        AudioSource source = CreateAudioSource();

        source.clip = soundEffects[soundIndex];
        source.Play();
    }

    private AudioSource CreateAudioSource()
    {
        GameObject GO = new GameObject();
        GO.name = "Audio source";
        GO.transform.parent = this.transform;

        AudioSource source = GO.AddComponent<AudioSource>();

        audioSources.Add(source);
        return source;
    }

}

[Serializable]
public struct Sounds
{
    public string clipName;
    public AudioClip audioClip;
}

