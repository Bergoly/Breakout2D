using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hitPoints = 1;

    public static event Action<Brick> OnBrickDestrucion;
    
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
            OnBrickDestrucion?.Invoke(this);
            Destroy(this.gameObject);
        }
        else
        {

        }
    }
}
