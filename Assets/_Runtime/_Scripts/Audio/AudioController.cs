using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameSaver gameSaver;

    private const float maxVolumeDb = 0;
    private const float minVolumeDb = -60f;
        
    public float MainVolumePercent
    {
        get {return GetFromVolumeToPercent(GetAudioMixerVolume(MAIN_VOLUME_KEY));}
        set {SetAudioMixerVolume(MAIN_VOLUME_KEY, value);} 
    }

    public float MusicVolumePercent
    {
        get {return GetFromVolumeToPercent(GetAudioMixerVolume(MUSIC_VOLUME_KEY));}
        set {SetAudioMixerVolume(MUSIC_VOLUME_KEY, value);} 
    }

    public float SFXVolumePercent
    {
        get {return GetFromVolumeToPercent(GetAudioMixerVolume(SFX_VOLUME_KEY));}
        set {SetAudioMixerVolume(SFX_VOLUME_KEY, value);} 
    }

    public const string MAIN_VOLUME_KEY = "MainVolume";
    public const string MUSIC_VOLUME_KEY = "MusicVolume";
    public const string SFX_VOLUME_KEY = "SFXVolume";

    private void Start() 
    {
        LoadAudioPreferences();    
    }

    private void LoadAudioPreferences()
    {
        gameSaver.LoadGame();
        MainVolumePercent = gameSaver.CurrentSaveData.SettingsData.masterVolume;
        MusicVolumePercent = gameSaver.CurrentSaveData.SettingsData.musicVolume;
        SFXVolumePercent = gameSaver.CurrentSaveData.SettingsData.SFXVolume;
        
    }

    private float GetAudioMixerVolume(string key)
    {
        float volumeDb = 1;
        if(audioMixer.GetFloat(key, out volumeDb))
        {
            return volumeDb;
        }
        return volumeDb;
    }

    private void SetAudioMixerVolume (string key, float percent)
    {
        percent = Mathf.Clamp01(percent);
        var volumeDb = Mathf.Lerp(minVolumeDb, maxVolumeDb, percent);
        audioMixer.SetFloat(key, volumeDb);
    }

    private float GetFromVolumeToPercent(float volume)
    {
        var value = Mathf.InverseLerp(minVolumeDb, maxVolumeDb, volume);

        return value;
    }

    private float GetFromPercentToVolume(float percent)
    {
        percent = Mathf.Clamp01(percent);
        float value = Mathf.Lerp(minVolumeDb, maxVolumeDb, percent);
        return value;
    }

    public void SaveSettings()
    {
        AudioPreferences data = new AudioPreferences()
        {
            masterVolume = MainVolumePercent,
            musicVolume = MusicVolumePercent,
            SFXVolume = SFXVolumePercent
        };
        
        gameSaver.SaveSettingsData(data);
    }

}
