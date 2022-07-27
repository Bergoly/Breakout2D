using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    SpriteRenderer sr;
    private BoxCollider2D boxCollider;
    
    public int hitPoints = 1;
    public ParticleSystem destroyEffect;

    public static event Action<Brick> OnBrickDestrucion;

 

    private void Awake()
    {
        this.sr = this.GetComponent<SpriteRenderer>();
        this.boxCollider = this.GetComponent<BoxCollider2D>();
        Ball.OnLightningBallEnable += OnLightningBallEnable;
        Ball.OnLightningBallDisable += OnLightningBallDisable;
        
    }

    private void OnLightningBallDisable(Ball obj)
    {
        if (this != null)
        {
            this.boxCollider.isTrigger = false;
        }
    }

    private void OnLightningBallEnable(Ball obj)
    {
        if (this != null)
        {
            this.boxCollider.isTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool instantKill = false;

        if (collision.collider.tag == "Ball")
        {
            Debug.Log("OnCollisionEnter2D");
            Ball ball = collision.gameObject.GetComponent<Ball>();
            instantKill = ball.isLightningBall;
        }

        if (collision.collider.tag == "Ball" || collision.collider.tag == "Projectile")
        {
            this.TakeDamage(instantKill);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        bool instantKill = false;

        if (collision.tag == "Ball")
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            instantKill = ball.isLightningBall;
        }

        if (collision.tag == "Ball" || collision.tag == "Projectile")
        {
            this.TakeDamage(instantKill);
        }
    }

    private void TakeDamage(bool instantKill)
    {
        
        this.hitPoints--;

        if(this.hitPoints <= 0 || instantKill)
        {
           
            BricksManager.Instance.RemainingBricks.Remove(this);
            OnBrickDestrucion?.Invoke(this);
            OnBrickDestroy();
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            //this.sr.sprite = BricksManager.Instance.Sprites[this.hitPoints - 1];
            this.sr.color = new Vector4(sr.color.r-0.2372f, sr.color.g-0.2372f, sr.color.b-0.2372f, 1);
        }
    }

    private void OnBrickDestroy()
    {
        float buffSpawnChance = UnityEngine.Random.Range(0, 100f);
        float debuffSpawnChance = UnityEngine.Random.Range(0, 100f);
        bool alreadySpawned = false;

        if (buffSpawnChance <= ItemsManager.Instance.BuffChance)
        {
            alreadySpawned = true;
            Item newBuff = this.SpawnItem(true);
        }

        if (debuffSpawnChance <= ItemsManager.Instance.DebuffChance && !alreadySpawned)
        {
            Item newDebuff = this.SpawnItem(false);
        }
    }

    private Item SpawnItem(bool isBuff)
    {
        List<Item> collection;

        if (isBuff)
        {
            collection = ItemsManager.Instance.AvailableBuffs;
        }
        else
        {
            collection = ItemsManager.Instance.AvailableDebuffs;
        }

        int buffIndex = UnityEngine.Random.Range(0, collection.Count);
        Item prefab = collection[buffIndex];
        Item newItem = Instantiate(prefab, this.transform.position, Quaternion.identity) as Item;

        return newItem;
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

    private void OnDisable()
    {
        Ball.OnLightningBallEnable -= OnLightningBallEnable;
        Ball.OnLightningBallDisable -= OnLightningBallDisable;
    }
}
