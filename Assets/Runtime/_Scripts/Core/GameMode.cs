using System.Collections;
using UnityEngine;
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
    private GameSaver gameSaver;
    private float startGameTime;
    private bool IsGameRunning;
    private float score = 0.0f;

    public int TemporaryScoreMultiplier
    {
        get => temporaryScoreMultiplier;
        set => temporaryScoreMultiplier = Mathf.Max(1, value);
    }

    private int temporaryScoreMultiplier = 1;

    private float currentPowerUpTime = 0;
    [SerializeField] public bool IsPowerUpMultiplierEnabled {get; private set;} = false;

    public int Score => Mathf.RoundToInt(score);
    public int TravelledDistance => Mathf.RoundToInt(player.transform.position.z - player.InitialPosition.z);
    public int CherriesPicked {get;set;}
    public int PeanutsPicked {get; set;}

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

            float extraScoreMultiplier = 1 + timePercent + TemporaryScoreMultiplier;
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

    #region GamePlay

    public void OnPauseGame()
    {
        Time.timeScale = 0;
    }

    public void OnResumeGame()
    {
        Time.timeScale = 1;
    }
    
    #endregion

    public void OnGameOver()
    {
        if(player.IsInvicible) return;
        int highestScore = Score > SaveGame.CurrentSaveData.ScoreStatusData.highestScore ?
         Score : SaveGame.CurrentSaveData.ScoreStatusData.highestScore;
        ScoreStatusData saveData = new ScoreStatusData()
        {
            highestScore = highestScore,
            lastScore = Score,
            totalCherriesPicked = SaveGame.CurrentSaveData.ScoreStatusData.totalCherriesPicked + CherriesPicked,
            totalPeanutsPicked = SaveGame.CurrentSaveData.ScoreStatusData.totalPeanutsPicked + PeanutsPicked
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
