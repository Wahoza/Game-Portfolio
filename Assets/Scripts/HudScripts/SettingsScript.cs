using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsScript : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    float MasterVolume = 0.4f;
    float MusicVolume = 0.3f;
    float SFXVolume = 0.2f;

    string masterExposed = "MasterVol";
    string musicExposed = "MusicVol";
    string sfxExposed = "SFXVol";

    [Header("Screen")]
    string FullScreenPref = "fullScreenPref";
    [SerializeField] Toggle FullScreenToggle;
    bool isFullScreen = false;
    Vector2 baseRes = new Vector2(1280, 720);

    public void InitSettings()
    {
        if ((PlayerPrefs.HasKey(musicExposed) && PlayerPrefs.HasKey(sfxExposed) && PlayerPrefs.HasKey(masterExposed)))
        {
            LoadVolume();
            SaveVolumeSettings();

        }
        else
        {
            masterSlider.value = MasterVolume;
            musicSlider.value = MusicVolume;
            SFXSlider.value = SFXVolume;

            SaveVolumeSettings();
        }

        if (false)//PlayerPrefs.HasKey(FullScreenPref))
        {
            LoadScreenSettings();
        }
        else
        {
            FullScreenToggle.isOn = isFullScreen;
            SetFullscreen();
        }
    }
    public void SetMasterVolume()
    {
        MasterVolume = masterSlider.value;
        Debug.Log("Master :" + masterSlider.value);
        mixer.SetFloat(masterExposed, Mathf.Log10(MasterVolume) * 20);
        PlayerPrefs.SetFloat(masterExposed, MasterVolume);
    }
    public void SetMusicVolume()
    {
        MusicVolume = musicSlider.value;
        mixer.SetFloat(musicExposed, Mathf.Log10(MusicVolume) * 20);
        PlayerPrefs.SetFloat(musicExposed, MusicVolume);
    }

    public void SetSFXVolume()
    {
        SFXVolume = SFXSlider.value;
        Debug.Log("SFX :" + SFXSlider.value);
        mixer.SetFloat(sfxExposed, Mathf.Log10(SFXVolume) * 20);
        PlayerPrefs.SetFloat(sfxExposed, SFXVolume);
    }
    public void LoadVolume()
    {
        MasterVolume = PlayerPrefs.GetFloat(masterExposed);
        masterSlider.value = MasterVolume;
        MusicVolume = PlayerPrefs.GetFloat(musicExposed);
        musicSlider.value = MusicVolume;
        SFXVolume = PlayerPrefs.GetFloat(sfxExposed);
        SFXSlider.value = SFXVolume;
    }
    public void SaveVolumeSettings()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void LoadScreenSettings()
    {
        isFullScreen = PlayerPrefs.GetInt(FullScreenPref) == 1 ? true : false;
        FullScreenToggle.isOn = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }
    public void SetFullscreen()
    {
        isFullScreen = FullScreenToggle.isOn;
        Screen.fullScreen = isFullScreen;
        PlayerPrefs.SetInt(FullScreenPref, isFullScreen ? 1 : 0);



        if (true)
        {
            Resolution displayResolution = Screen.currentResolution;
            //float aspectRatio = (float)displayResolution.width / displayResolution.height;

            Screen.SetResolution(Screen.width, Screen.height, isFullScreen);
        }
        else
        {
            //Resolution displayResolution = Screen.currentResolution;
            //displayResolution.width = (int)baseRes.x;
            //displayResolution.height = (int)baseRes.y;

            Screen.SetResolution((int)baseRes.x, (int)baseRes.y, false);
        }
    }
    public void SaveScreenSettings()
    {
        SetFullscreen();
    }
}

