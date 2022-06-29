using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    SpriteRenderer sr;
    
    public int hitPoints = 1;
    public ParticleSystem destroyEffect;

   

    public static event Action<Brick> OnBrickDestrucion;

    private void Awake()
    {
        this.sr = this.GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
    }

    private void ApplyCollisionLogic(Ball ball)
    {
        this.hitPoints--;

        if(hitPoints <= 0)
        {
            BricksManager.Instance.RemainingBricks.Remove(this);
            OnBrickDestrucion?.Invoke(this);
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            //this.sr.sprite = BricksManager.Instance.Sprites[this.hitPoints - 1];
            this.sr.color = new Vector4(sr.color.r-0.2372f, sr.color.g-0.2372f, sr.color.b-0.2372f, 1);
        }
    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(brickPos.x, brickPos.y, brickPos.z - 0.2f);
        GameObject effect = Instantiate(destroyEffect.gameObject, spawnPos, Quaternion.identity);

        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = new Color(0.1f, 0.1f, 0.1f, 1);
        Destroy(effect, destroyEffect.main.startLifetime.constant);
    }

    public void Init(Transform containerTransform, int hitpoints)
    {
        this.transform.SetParent(containerTransform);
        this.hitPoints = hitpoints;
    }
}
