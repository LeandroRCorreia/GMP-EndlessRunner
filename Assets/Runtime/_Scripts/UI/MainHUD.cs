using System.Collections;
using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameMode gameMode;

    [Header("Overlays")]
    [SerializeField] private GameObject hudOverlay;
    [SerializeField] private CountDownOverlay countDownStartGameOverlay;
    [SerializeField] private GameObject pauseOverlay;
    [SerializeField] private StartGameOverlay waitStartGameOverlay;
    [SerializeField] private GameObject settingsOverlay;

    private void Start() 
    {
        ShowWaitForStartGameOverlay();    
    }
    
    public IEnumerator PerformCountDown(int startGameDelay)
    {
        countDownStartGameOverlay.gameObject.SetActive(true);
        waitStartGameOverlay.gameObject.SetActive(false);
        hudOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        settingsOverlay.SetActive(false);

        yield return StartCoroutine(countDownStartGameOverlay.PerformCountDown(startGameDelay));
    }

    public void ShowWaitForStartGameOverlay()
    {
        waitStartGameOverlay.gameObject.SetActive(true);
        hudOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        settingsOverlay.SetActive(false);
        countDownStartGameOverlay.gameObject.SetActive(false);
        gameMode.OnWaitForStartGame();

    }

    public void ShowSettingsOverlay()
    {
        settingsOverlay.SetActive(true);
        waitStartGameOverlay.gameObject.SetActive(false);
        hudOverlay.SetActive(false);
        pauseOverlay.SetActive(false);
        countDownStartGameOverlay.gameObject.SetActive(false);

    }

    public void ShowHUDOverlay()
    {
        hudOverlay.SetActive(true);
        pauseOverlay.SetActive(false);
        waitStartGameOverlay.gameObject.SetActive(false);
        settingsOverlay.SetActive(false);
        countDownStartGameOverlay.gameObject.SetActive(false);

        gameMode.OnResumeGame();

    }

    public void ShowPauseOverlay()
    {
        pauseOverlay.SetActive(true);
        hudOverlay.SetActive(false);
        waitStartGameOverlay.gameObject.SetActive(false);
        settingsOverlay.SetActive(false);
        countDownStartGameOverlay.gameObject.SetActive(false);

        gameMode.OnPauseGame();

    }

}
