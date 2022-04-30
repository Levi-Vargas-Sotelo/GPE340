using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("Player Attributes")]
    public Player thePlayer;
    public Player playerPrefab;
    [SerializeField]
    public int playerLives;
    public int playerScore;
    [SerializeField]
    private Transform playerSpawnPoint;
    [SerializeField]
    private TestHealthText healthUI;
    [Header("Game Settings")]
    public bool isPaused;
    [SerializeField]
    private float spawnDelay;
    public Controller playerCont;
    
    [SerializeField]
    public float sFXVolume;
    [SerializeField]
    public float musicVolume;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AnimationCurve volumeToDB;
    [SerializeField]

    public UnityEvent RespawnPlayer;
    [SerializeField]
    public UnityEvent SpawnPlayer;
    public UnityEvent onPause;
    public UnityEvent onResume;
    public UnityEvent gameOver;
    public UnityEvent onWin;
    
    

    void Awake ()
    {
        Instance = this;
        isPaused = false;
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                PauseGame();
            }
        }
  
    }

    public void Spawn()
    {
        thePlayer = Instantiate(playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation) as Player;
        thePlayer.playerHealth.OnDeath.AddListener (HandlePlayerDeath);
        playerCont.player = thePlayer;
        SpawnPlayer.Invoke();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        Instance.onPause.Invoke();
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        Instance.onResume.Invoke();
    }

    public void Continue()
    {
        isPaused = true;
        Time.timeScale = 0f;
        SceneManager.LoadScene ("MainMenu");
    }

    public void GameOver()
    {
        gameOver.Invoke();
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        onWin.Invoke();
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HandlePlayerDeath()
    {
        if (playerLives > 0)
        {
            thePlayer.playerHealth.OnDeath.RemoveListener (HandlePlayerDeath);
            StartCoroutine (RespawnThePlayer());
            playerLives--;
        }
        else
        {
            GameOver();
        }
    }

    IEnumerator RespawnThePlayer()
    {
        yield return new WaitForSeconds (spawnDelay);
        RespawnPlayer.Invoke();
    }

    public void SetMusic(float value)
    {
        audioMixer.SetFloat ("Music Volume", Mathf.Log10(value) * 20);
    }

    public void SetSFX(float value)
    {
        audioMixer.SetFloat ("SFX Volume", Mathf.Log10(value) * 20);
    }
}
