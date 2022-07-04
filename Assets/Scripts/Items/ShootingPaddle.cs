using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPaddle : Item
{
    protected override void ApplyEffect()
    {
        Paddle.Instance.StartShooting();
    }
}
