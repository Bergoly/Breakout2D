using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    Camera _mainCamera;
    float _paddleInitialY;
    float _defaultPaddleWidthInPixels = 145;
    float _defaultLeftClamp = 310;
    float _defaultRightClamp = 2250;
    SpriteRenderer _sr;

    private void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _paddleInitialY = this.transform.position.y;
        _sr = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        PaddleMovement(); 
    }

    void PaddleMovement()
    {
        float paddleShift = (_defaultPaddleWidthInPixels - ((_defaultPaddleWidthInPixels / 2) * this._sr.size.x));
        float leftClamp = _defaultLeftClamp - paddleShift;
        float rightClamp = _defaultRightClamp + paddleShift;
        float mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mousePositionWorldX = _mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;   
        this.transform.position = new Vector3(mousePositionWorldX, _paddleInitialY, 0);
    }
}
