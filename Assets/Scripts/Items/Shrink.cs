using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : Item
{
    public float NewWidth = 1.0f;

    protected override void ApplyEffect()
    {
        if (Paddle.Instance != null && !Paddle.Instance.PaddleIsTransforming)
        {
            AudioManager.Instance.effectPlayer.PlayOneShot(AudioManager.Instance.shrink);
            Paddle.Instance.StartWidthAnimation(NewWidth);
        }
    }
}