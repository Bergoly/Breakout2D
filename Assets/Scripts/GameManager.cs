using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    static GameManager _instance;

    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public GameObject pauseScreen;
    
    public bool IsGameStarted { get; set; }

    
    public int AvailableLives= 3;


    public int Lives { get; set; }

    public static event Action<int> OnLiveLost;

    private void Start()
    {
        this.Lives = this.AvailableLives; 
        //Screen.SetResolution(1920, 800, false);
        Ball.OnBallDeath += OnBallDeath;
        Brick.OnBrickDestrucion += OnBrickDestruction;
    }

    private void OnBrickDestruction(Brick obj)
    {
        if (BricksManager.Instance.RemainingBricks.Count <= 0)
        {
            BallsManager.Instance.ResetBalls();
            Instance.IsGameStarted = false;
            BricksManager.Instance.LoadNextLevel();

        }
    }

    public void RestartGame()
    {
        this.Lives = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnBallDeath(Ball obj)
    {
        if(BallsManager.Instance.Balls.Count <= 0)
        {
            this.Lives--;

            if(this.Lives < 1)
            {
                gameOverScreen.SetActive(true);
                Cursor.visible = true;
            }
            else
            {
                OnLiveLost?.Invoke(this.Lives);
                BallsManager.Instance.ResetBalls();
                IsGameStarted = false;
                //BricksManager.Instance.LoadLevel(BricksManager.Instance.CurrentLevel);
            }
        }
    }

    internal void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }

    private void Update()
    {
        if (!pauseScreen.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            Cursor.visible = false;
        }
    }
}
