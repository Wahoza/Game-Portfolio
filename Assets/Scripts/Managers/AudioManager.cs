using UnityEngine;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using System.Runtime.CompilerServices;
public class AudioManager : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource sfxBoatSource;

    [SerializedDictionary("Audio Name", "Audio Clip")]
    public SerializedDictionary<string, AudioClip> sfxNameClipDict;
    [SerializedDictionary("Music Name", "Music Clip")]
    public SerializedDictionary<string, AudioClip> musicNameClipDict;

    [SerializeField] AudioClip boatSounds;

    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            sfxBoatSource.clip = boatSounds;
        }
    }
    public void PlaySFX(string clipName)
    {
        if (sfxNameClipDict.ContainsKey(clipName))
        {
            if (clipName == "Sploosh")
            {
                if (GameManager.Instance.GetPlayer().GetComponent<BoatMover>().controllsEnabled) {
                    sfxSource.PlayOneShot(sfxNameClipDict[clipName]);

                }
            }
            else
            {
                sfxSource.PlayOneShot(sfxNameClipDict[clipName]);

            }
        }
        else
        {
            Debug.Log("Couldnt find clip: " + clipName);
        }
    }

    public void PlayMusic(string musicName)
    {
        if (musicNameClipDict.ContainsKey(musicName))
        {
            musicSource.clip = musicNameClipDict[musicName];
            musicSource.Play();
        }
        else
        {
            Debug.Log("Couldnt find music");
        }
    }
    public void PlayBoatSounds()
    {
        sfxBoatSource.Play();
    }    
    public void PauseBoatSounds()
    {
        sfxBoatSource.Pause();
    }

    public void SetBoatPitch(float pitch)
    {
        sfxBoatSource.pitch = pitch;
    }
    public void PauseMusic()
    {
        musicSource.Pause();
    }    
    public void ResumeMusic()
    {
        musicSource.Play();
    }
}
