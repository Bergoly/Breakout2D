using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : Item
{
    protected override void ApplyEffect()
    {
        foreach (var ball in BallsManager.Instance.Balls)
        {
            ball.StartLightningBall();
        }
    }

  
}
