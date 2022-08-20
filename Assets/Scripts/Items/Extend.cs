using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extend : Item
{
    public float NewWidth = 2.5f;

    protected override void ApplyEffect()
    {
        if (Paddle.Instance != null && !Paddle.Instance.PaddleIsTransforming)
        {
            AudioManager.Instance.effectPlayer.PlayOneShot(AudioManager.Instance.grow);
            Paddle.Instance.StartWidthAnimation(NewWidth);
        }
    }
}
