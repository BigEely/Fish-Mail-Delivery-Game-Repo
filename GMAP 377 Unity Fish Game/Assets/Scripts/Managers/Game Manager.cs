using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles game, aka level loading and saving/wiping data.

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] EndOfDaySummaryScript endOfDaySummaryScript;

    [Header("State Data")]
    [SerializeField] GameState currentGameState = GameState.mainMenu;
    [HideInInspector]
    public GameState previousState; //Used for handling pausing

    [Header("Save/Load Settings")]
    public int TotalFishFlakes = 0; //Change save data variables in PlayerDataScript.cs
    public int BaseSwimSpeedLevel = 1;
    public int FastSwimSpeedLevel = 1;
    public int MaxStaminaLevel = 1;
    public int CurrentDay = 1;

    [Header("Game Data")]
    public int fishFlakesCollected = 0;
    public int timeRemaining = 0;
    public int packageFishFlakes = 0; //achieved
    public int totalPackageReward = 0; //achieved - total = package fails (should be negative if lost a package)
    private bool hasCalculated = false;

    public enum GameState
    {
        mainMenu,
        game,
        endOfDay,
        pause
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        StartUpGame();
        LoadGame(); //Delete if no save system
    }

    private void Start()
    {
        previousState = currentGameState;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    { 
        endOfDaySummaryScript = FindFirstObjectByType<EndOfDaySummaryScript>();
        if (endOfDaySummaryScript != null)
        {
            endOfDaySummaryScript.gameObject.SetActive(false);
        }
    }

    private void StartUpGame()
    { 
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    private void endDay()
    {
        if (endOfDaySummaryScript == null)
        {
            Debug.LogWarning("EndofDaySummaryScript not found");
            return;
        }

        endOfDaySummaryScript.gameObject.SetActive(true);
        endOfDaySummaryScript.CalculateSummary(fishFlakesCollected, timeRemaining, packageFishFlakes, totalPackageReward);
        fishFlakesCollected = 0;
        timeRemaining = 0;
        packageFishFlakes = 0;
        totalPackageReward = 0;
        CurrentDay += 1;
        SaveGame();
    }

    #region GameState Functions
    public void switchState(GameState newGameState) //How to use: GameManager.instance.switchState(GameManager.GameState._State You Want_);
    {
        previousState = currentGameState;
        currentGameState = newGameState;
        switch (newGameState)
        {
            case GameState.mainMenu:
                //StartCoroutine(SoundManager.MusicTransition(SoundType._Insert Menu Music_));
                SaveGame();
                break;
            case GameState.game:
                //StartCoroutine(SoundManager.MusicTransition(SoundType._Insert Game Music_));
                hasCalculated = false;
                SaveGame();
                break;
            case GameState.endOfDay:
                if (!hasCalculated)
                {
                    endDay();
                    hasCalculated = true;
                }

                break;
            case GameState.pause:
                SoundManager.onPauseMusic();

                break;
        }
    }

    public GameState getCurrentGameState()
    { 
        return currentGameState;
    }

    public void Pause()
    {
        SoundManager.onPauseMusic();
        switchState(GameState.pause);
        Time.timeScale = 0f;
    }

    public void Unpause(GameState previousState)
    {
        SoundManager.onUnpauseMusic();
        switchState(previousState);
        Time.timeScale = 1f;
    }
    #endregion

    #region Save/Load //Can Delete if no save system
    public void SaveGame()
    {
        SaveScript.Save(this);
    }

    public void LoadGame()
    {
        PlayerDataScript data = SaveScript.Load();
        if (data != null)
        {
            TotalFishFlakes = data.TotalFishFlakes;
            BaseSwimSpeedLevel = data.BaseSwimSpeedLevel;
            FastSwimSpeedLevel = data.FastSwimSpeedLevel;
            MaxStaminaLevel = data.MaxStaminaLevel;
            CurrentDay = data.CurrentDay;
        }
    }

    public void WipeData()
    {
        SaveScript.Delete();
        TotalFishFlakes = 0;
        BaseSwimSpeedLevel = 1;
        FastSwimSpeedLevel = 1;
        MaxStaminaLevel = 1;
        CurrentDay = 1;
        SaveScript.Save(this);
    }
    #endregion
}
