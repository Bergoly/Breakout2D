using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    #region Singleton

    static BallsManager _instance;


    public static BallsManager Instance => _instance;

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

    [SerializeField]
    Ball ballPrefab;
    
    Ball initialBall;

    Rigidbody2D initialBallRb;

    public float initialBallSpeed = 250; 

    public List<Ball> Balls { get; set; }



    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + 0.27f, 0);
            initialBall.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0))
            {
                initialBallRb.isKinematic = false;
                initialBallRb.AddForce(new Vector2(0, initialBallSpeed));
                GameManager.Instance.IsGameStarted = true;
            }
        }
    }

    public void SpawnBalls(Vector3 position, int count, bool isLightningBall)
    {
        AudioManager.Instance.audioSource.PlayOneShot(AudioManager.Instance.multiBall);
        for (int i = 0; i < count; i++)
        {
            Ball spawnedBall = Instantiate(ballPrefab, position, Quaternion.identity) as Ball;
            if (isLightningBall)
            {
                spawnedBall.StartLightningBall();
            }
            Rigidbody2D spawnedBallRb = spawnedBall.GetComponent<Rigidbody2D>();
            spawnedBallRb.isKinematic = false;
            spawnedBallRb.AddForce(new Vector2(0, initialBallSpeed));
            this.Balls.Add(spawnedBall);
        }
    }

    public void ResetBalls()
    {
        foreach (var ball in this.Balls.ToList())
        {
            Destroy(ball.gameObject);
        }
        InitBall();
    }

    void InitBall()
    {
        Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + 0.27f, 0);
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();

        this.Balls = new List<Ball>()
        {
            initialBall
        };
    }
}
