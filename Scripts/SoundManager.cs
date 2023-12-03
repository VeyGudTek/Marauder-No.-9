using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] soundClips;
    public Sound[] musicClips;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource sfxStateSource;
    public AudioSource sfxShield;
    [SerializeField] private GameManager gm;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SoundManager.Instance.playMusic("Menu");
    }

    private void Update()
    {
        musicSource.volume = gm.GetMusicVolume() / 10;
        sfxStateSource.volume = gm.GetSFXVolume() / 10;
        sfxShield.volume = gm.GetSFXVolume() / 10;
    }
    public void playSound(string soundName)
    {
        Sound s = Array.Find(soundClips, x => x.name == soundName);

        if (s == null)
        {
            Debug.Log("No Sound with name: " + soundName);
        }
        else
        {
            sfxSource.PlayOneShot(s.audio, gm.GetSFXVolume()/10);
        }
    }

    public void playMusic(string musicName)
    {
        Sound s = Array.Find(musicClips, x => x.name == musicName);

        musicSource.Stop();
        if (s == null)
        {
            Debug.Log("No Music with name: " + musicName);
        }
        else
        {
            musicSource.clip = s.audio;
            musicSource.Play();
        }
    }

    public void playStateSound(string soundName)
    {
        Sound s = Array.Find(soundClips, x => x.name == soundName);
        

        if (s == null)
        {
            Debug.Log("No Sound with name: " + soundName);
        }
        else if (!sfxStateSource.isPlaying)
        {
            sfxStateSource.clip = s.audio;
            sfxStateSource.Play();
        }
    }

    public void stopStateSound()
    {
        sfxStateSource.Stop();
    }

    public void playShieldSound()
    {
        Sound s = Array.Find(soundClips, x => x.name == "Shield");

        if (s == null)
        {
            Debug.Log("No Sound with name: " + "Shield");
        }
        else if (!sfxShield.isPlaying)
        {
            sfxShield.clip = s.audio;
            sfxShield.Play();
        }
    }

    public void stopShieldSound()
    {
        sfxShield.Stop();
    }
}

