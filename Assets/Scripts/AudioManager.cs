using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume = PlayerPrefs.GetFloat("SoundVolume");

            s.source.loop = s.loop;

            if (s.name == "Theme")
            {
                s.source.volume = s.volume = PlayerPrefs.GetFloat("MusicVolume");
            }
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void OffMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", 0f);

        foreach (Sound s in sounds)
        {
            if (s.name == "Theme")
            {
                s.source.volume = s.volume = PlayerPrefs.GetFloat("MusicVolume");
            }
        }
    }

    public void OnMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", 1f);

        foreach (Sound s in sounds)
        {
            if (s.name == "Theme")
            {
                s.source.volume = s.volume = PlayerPrefs.GetFloat("MusicVolume");
            }
        }
    }

    public void OffSound()
    {
        PlayerPrefs.SetFloat("SoundVolume", 0f);

        foreach (Sound s in sounds)
        {
            if (s.name == "Theme")
            {
                continue;
            }
            else
            {
                s.source.volume = s.volume = PlayerPrefs.GetFloat("SoundVolume");
            }
        }
    }

    public void OnSound()
    {
        PlayerPrefs.SetFloat("SoundVolume", 1f);

        foreach (Sound s in sounds)
        {
            if (s.name == "Theme")
            {
                continue;
            }
            else
            {
                s.source.volume = s.volume = PlayerPrefs.GetFloat("SoundVolume");
            }
        }
    }
}
