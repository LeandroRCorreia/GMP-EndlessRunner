using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsOverlay : MonoBehaviour
{
    [SerializeField] private GameSaver gameSaver;
    [SerializeField] private AudioController audioController;

    [Space]

    [Header("Ui Elements")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Button deleteDataButton;
    [SerializeField] private TextMeshProUGUI deleteDataTxt;

    private void OnEnable() 
    {
        deleteDataButton.interactable = true;
        deleteDataTxt.text = "Delete Data";
        UpdateUi();

    }

    private void OnDisable() 
    {
        audioController.SaveSettings();    
    }

    private void UpdateUi()
    {
        masterSlider.value = audioController.MainVolumePercent;
        musicSlider.value = audioController.MusicVolumePercent;
        SFXSlider.value = audioController.SFXVolumePercent;

    }

    public void OnMainVolumeChange (float percent)
    {
        audioController.MainVolumePercent = masterSlider.value;
    }

    public void OnMusicVolumeChange (float percent)
    {
        audioController.MusicVolumePercent = musicSlider.value;
    }

    public void OnSFXVolumeChange (float percent)
    {
        audioController.SFXVolumePercent = SFXSlider.value;
    }

    public void DeleteScoreData()
    {
        deleteDataButton.interactable = false;
        deleteDataTxt.text = "Deleted!";
        gameSaver.ResetScoreData();
    }

}
