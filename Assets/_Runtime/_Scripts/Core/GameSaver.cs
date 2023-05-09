using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ScoreStatusData
{
    public int highestScore;
    public int lastScore;
    public int totalCherriesPicked;
}

public class AudioPreferences
{
    public float masterVolume;
    public float musicVolume;
    public float SFXVolume;

}

public class SaveData
{
    public ScoreStatusData ScoreStatusData;
    public AudioPreferences SettingsData;

}

public class GameSaver : MonoBehaviour
{
    public int GetHighestScore => PlayerPrefs.GetInt(SaveGameKeys.HIGHEST_SCORE_KEY, 0);
    public int GetLastScore => PlayerPrefs.GetInt(SaveGameKeys.LAST_SCORE_KEY, 0);
    public int GetTotalCherriesPicked => PlayerPrefs.GetInt(SaveGameKeys.TOTAL_CHERRIES_PICKED_KEY, 0);

    public SaveData CurrentSaveData {get; private set;}

    private bool IsLoaded => CurrentSaveData != null ? CurrentSaveData.ScoreStatusData != null && CurrentSaveData.SettingsData != null : false;

    public void SaveScoreData(ScoreStatusData data)
    {
        PlayerPrefs.SetInt(SaveGameKeys.HIGHEST_SCORE_KEY, data.highestScore);
        PlayerPrefs.SetInt(SaveGameKeys.TOTAL_CHERRIES_PICKED_KEY, data.totalCherriesPicked);
        PlayerPrefs.SetInt(SaveGameKeys.LAST_SCORE_KEY, data.lastScore);
        PlayerPrefs.Save();
        CurrentSaveData.ScoreStatusData = data;
    }

    public void SaveSettingsData(AudioPreferences data)
    {
        PlayerPrefs.SetFloat(SaveGameKeys.MAIN_VOLUME_KEY, data.masterVolume);
        PlayerPrefs.SetFloat(SaveGameKeys.MUSIC_VOLUME_KEY, data.musicVolume);
        PlayerPrefs.SetFloat(SaveGameKeys.SFX_VOLUME_KEY, data.SFXVolume);
        PlayerPrefs.Save();
        CurrentSaveData.SettingsData = data;
    }

    public void LoadGame()
    {
        if(IsLoaded) return;
        ScoreStatusData scoreData = new ScoreStatusData()
        {
            highestScore = GetHighestScore,
            lastScore = GetLastScore,
            totalCherriesPicked = GetTotalCherriesPicked
        };
        AudioPreferences settingsData = new AudioPreferences()
        {
            masterVolume = PlayerPrefs.GetFloat(SaveGameKeys.MAIN_VOLUME_KEY, 1f),
            musicVolume = PlayerPrefs.GetFloat(SaveGameKeys.MUSIC_VOLUME_KEY, 0.75f),
            SFXVolume = PlayerPrefs.GetFloat(SaveGameKeys.SFX_VOLUME_KEY, 0.75f),
        };
        CurrentSaveData = new SaveData()
        {
            ScoreStatusData = scoreData,
            SettingsData = settingsData
        };

    }

    public void ResetScoreData()
    {
        PlayerPrefs.DeleteAll();
        CurrentSaveData = null;
        LoadGame();
        
    }

}
