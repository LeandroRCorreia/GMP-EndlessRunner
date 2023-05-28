using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class ScoreStatusData
{
    public int highestScore = 0;
    public int lastScore = 0;
    public int totalCherriesPicked = 0;
    public int totalPeanutsPicked = 0;
}

public class AudioPreferencesData
{
    public float masterVolume = 0.5f;
    public float musicVolume = 0.5f;
    public float SFXVolume = 0.5f;

}

public class SaveData
{
    public ScoreStatusData ScoreStatusData;
    public AudioPreferencesData SettingsData;

}

public class GameSaver : MonoBehaviour
{
    public SaveData CurrentSaveData {get; private set;}

    private bool IsLoaded => CurrentSaveData != null ? CurrentSaveData.ScoreStatusData != null && CurrentSaveData.SettingsData != null : false;
    
    private string DataOriginPath => $"{Application.persistentDataPath}" + "/Data";
    private string PreferencesAudioDataPath => DataOriginPath + "/PreferencesAudioData.txt";
    private string ScoreStatusDataPath => DataOriginPath + "/ScoreStatusData.txt";

    private void Awake()
    {
        CheckIfDataOriginDirectoryExists();
    }

    private void CheckIfDataOriginDirectoryExists()
    {
        if (!Directory.Exists(DataOriginPath))
        {
            Directory.CreateDirectory(DataOriginPath);
        }
    }

    public void SaveScoreData(ScoreStatusData data)
    {
        Serialized<ScoreStatusData>(ScoreStatusDataPath, data);
        CurrentSaveData.ScoreStatusData = data;
    }

    public void SaveSettingsData(AudioPreferencesData data)
    {
        Serialized<AudioPreferencesData>(PreferencesAudioDataPath, data);
        CurrentSaveData.SettingsData = data;
    }

    public void LoadGame()
    {
        if(IsLoaded) return;
        
        ScoreStatusData scoreData = Deserialize<ScoreStatusData>(ScoreStatusDataPath);
        AudioPreferencesData settingsData = Deserialize<AudioPreferencesData>(PreferencesAudioDataPath);

        CurrentSaveData = new SaveData()
        {
            ScoreStatusData = scoreData,
            SettingsData = settingsData
        };

    }

    private void Serialized<T>(string path, T data) where T : class
    {
        JsonSerializer serializer = new JsonSerializer();

        using(FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        using(StreamWriter file = new StreamWriter(stream))
        using(JsonWriter writer = new JsonTextWriter(file))
        {
            serializer.Serialize(writer, data);
        }

    }

    private T Deserialize<T>(string path) where T: class, new()
    {
        JsonSerializer serializer = new JsonSerializer();

        if(!File.Exists(path))
        {
            T data = new T();
            Serialized<T>(path, data);
            return data;
        }
        else
        {   
            using(FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using(StreamReader file = new StreamReader(stream))
            using(JsonReader reader = new JsonTextReader(file))
            {   
                T data = serializer.Deserialize<T>(reader);

                return data;
            }
        }

    }

    public void ResetScoreData()
    {
        DeleteAllData();
        CurrentSaveData = null;
        LoadGame();
    }

    private void DeleteAllData()
    {
        Directory.Delete(DataOriginPath, true);
        CheckIfDataOriginDirectoryExists();
        
    }

}
