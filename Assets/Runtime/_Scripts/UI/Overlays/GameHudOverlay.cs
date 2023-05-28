using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameHudOverlay : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI travelledDistanceTxt;
    [SerializeField] private TextMeshProUGUI cherriesPickedText;
    [SerializeField] private TextMeshProUGUI peanutPickedText;
    [SerializeField] private Image MultiplierIcon;

    private void LateUpdate() 
    {
        scoreTxt.text = $"Score : {gameMode.Score}";
        travelledDistanceTxt.text = $"{gameMode.TravelledDistance}m";
        cherriesPickedText.text = $"{gameMode.CherriesPicked}";
        peanutPickedText.text = $"{gameMode.PeanutsPicked}";
        MultiplierIcon.gameObject.SetActive(gameMode.TemporaryScoreMultiplier > 1);
    }

}
