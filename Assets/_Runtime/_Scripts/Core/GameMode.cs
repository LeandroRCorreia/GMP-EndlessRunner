using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(GameSaver))]
public class GameMode : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerController player;
    [SerializeField] private PlayerAnimationController playerAnimation;
    [SerializeField] private MusicPlayer musicPlayer;

    [Space]

    [Header("Gameplay")]
    [SerializeField] private float scoreBaseMultiplier = 1.5f;
    [Range(0, 5)]
    [SerializeField] private int startGameDelay = 3;
    [Range(0, 5)]
    [SerializeField] private int reloadGameDelay = 3;
    [SerializeField] private float startPlayerSpeed;
    [SerializeField] private float maxSpeedPlayer = 17;
    [SerializeField] private float timeToReachMaxSpeedInSeconds = 300f;
    private float startGameTime;
    private float score = 0.0f;
    public int Score => Mathf.RoundToInt(score);
    public int TravelledDistance => Mathf.RoundToInt(player.transform.position.z - player.InitialPosition.z);
    private bool IsGameRunning;
    public int CherriesPicked {get; private set;}

    private GameSaver gameSaver;

    [Space]

    [Header("UI")]
    [SerializeField] private MainHUD mainHUD;
    private float timeRunSeconds;

    public GameSaver SaveGame => gameSaver;

    private void Awake() 
    {
        gameSaver = GetComponent<GameSaver>();

    }

    private void Start() 
    {
        SaveGame.LoadGame();
    }   

    private void LateUpdate() 
    {
        if(IsGameRunning)
        {   
            timeRunSeconds += Time.deltaTime;
            float timePercent = timeRunSeconds / timeToReachMaxSpeedInSeconds;
            player.ForwardSpeed = Mathf.Lerp(startPlayerSpeed, maxSpeedPlayer, timePercent);

            float extraScoreMultiplier = 1 + timePercent;
            score += extraScoreMultiplier * scoreBaseMultiplier * player.ForwardSpeed * Time.deltaTime;
        }

    }

    public void OnWaitForStartGame()
    {
        player.enabled = false;
        musicPlayer.PlayMenuTheme();
    }

    public void ProcessStartGame()
    {
        StartCoroutine(StartGameCor());
    }

    private IEnumerator StartGameCor()
    {
        musicPlayer.StopMusic();
        yield return StartCoroutine(mainHUD.PerformCountDown(startGameDelay));
        yield return StartCoroutine(playerAnimation.PlayStartGameAnimation());
        OnStartGame();
    }

    private void OnStartGame()
    {
        mainHUD.ShowHUDOverlay();
        musicPlayer.PlayMainMusic();
        player.enabled = true;
        startGameTime = Time.time;
        IsGameRunning = true;
    }

    public void OnPauseGame()
    {
        Time.timeScale = 0;
    }

    public void OnCherriesPickup()
    {
        CherriesPicked++;
    }

    public void OnResumeGame()
    {
        Time.timeScale = 1;
    }

    public void OnGameOver()
    {
        int highestScore = Score > SaveGame.CurrentSaveData.ScoreStatusData.highestScore ?
         Score : SaveGame.CurrentSaveData.ScoreStatusData.highestScore;
        ScoreStatusData saveData = new ScoreStatusData()
        {
            highestScore = highestScore,
            lastScore = Score,
            totalCherriesPicked = SaveGame.CurrentSaveData.ScoreStatusData.totalCherriesPicked + CherriesPicked
        };
        gameSaver.SaveScoreData(saveData);

        musicPlayer.PlayDeathMusic();
        IsGameRunning = false;
        StartCoroutine(ReloadGameCoroutine());
    }

    private IEnumerator ReloadGameCoroutine()
    {
        yield return new WaitForSeconds(reloadGameDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }

}
