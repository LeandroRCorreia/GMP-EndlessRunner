using UnityEngine;
using TMPro;

public class StartGameOverlay : MonoBehaviour
{
    
    [SerializeField] private GameMode gameMode;

    [Header("Status Elements")]
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;
    [SerializeField] private TextMeshProUGUI totalCherriesPickedText;



//TODO: when overlay is actived in scene, this Unity function is not called
    private void OnEnable() 
    {
        gameMode.SaveGame.LoadGame();
        UpdateUI();
    }
//
    private void UpdateUI()
    {
        highestScoreText.text = $"Highest Score\n{gameMode.SaveGame.CurrentSaveData.ScoreStatusData.highestScore}";
        lastScoreText.text = $"Last Score\n{gameMode.SaveGame.CurrentSaveData.ScoreStatusData.lastScore}";
        totalCherriesPickedText.text = $"{gameMode.SaveGame.CurrentSaveData.ScoreStatusData.totalCherriesPicked}";
    }


}
