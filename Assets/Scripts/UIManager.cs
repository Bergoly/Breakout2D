using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text TargetText;
    public Text ScoreText;
    public Text LifesText;

    public int Score { get; set; }

    public void Awake()
    {
        Brick.OnBrickDestrucion += OnBrickDestruction;
        BricksManager.OnLevelLoaded += OnLevelLoaded;
        GameManager.OnLifeLost += OnLifeLost;
    }

    private void Start()
    {
      
        OnLifeLost(GameManager.Instance.totalLifes);
    }

    private void OnLifeLost(int remainingLifes)
    {
        LifesText.text = $"LIFES: {remainingLifes-1}";
    }

    private void OnLevelLoaded()
    {
        UpdateRemainingBricksText();
        UpdateScoreText(0);
    }

    private void UpdateScoreText(int increment)
    {
        this.Score += increment;
        string scoreString = this.Score.ToString().PadLeft(5, '0');
        ScoreText.text = $"Score:{Environment.NewLine}{scoreString}";
    }

    private void OnBrickDestruction(Brick obj)
    {
        UpdateRemainingBricksText();
        UpdateScoreText(10);
    }

    private void UpdateRemainingBricksText()
    {
        TargetText.text = $"TARGET:{Environment.NewLine}{BricksManager.Instance.RemainingBricks.Count} / {BricksManager.Instance.InitialBricksCount}";
    }

    private void OnDisable()
    {
        Brick.OnBrickDestrucion -= OnBrickDestruction;
        BricksManager.OnLevelLoaded -= OnLevelLoaded;
    }
}
