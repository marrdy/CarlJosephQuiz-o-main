using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class SMScript : MonoBehaviour
{
    public float musicVolume = 1;
    public float sfxVolume = 1;
    public float vlvolume = 1;
    public Sounds[] SoundTracks;
    public static SMScript instance;
    public Slider musicSlider;
    public Slider SFX;


    void Awake()
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
        foreach (Sounds s in SoundTracks)
        {
            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }

    }

    void Start()
    {
        playtrack("Theme");
        musicSlider.value = musicVolume;
        SFX.value = sfxVolume;
    }
    public void playtrack(string name)
    {
        Sounds s = Array.Find(SoundTracks, Sounds => Sounds.name == name);
        s.source.Play();
    }
    public void MusicVolume()
    {

        foreach (Sounds s in SoundTracks)
        {

            if (s.type == Sounds.SoundType.Music)
            {
                s.source.volume = musicSlider.value;
            }
        }
        musicVolume = musicSlider.value;
    }

    public void SFXVolume()
    {

        foreach (Sounds s in SoundTracks)
        {

            if (s.type == Sounds.SoundType.SFX)
            {
                s.source.volume = SFX.value;
            }
        }
       
        sfxVolume = SFX.value;
    }

}

[System.Serializable]
public class Sounds
{
    public enum SoundType
    {
        Music,
        SFX
    }
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
    public SoundType type;
    public bool loop;
}
