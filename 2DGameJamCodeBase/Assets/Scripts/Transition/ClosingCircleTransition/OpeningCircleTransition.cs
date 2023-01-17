using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningCircleTransition : Transition
{
    [SerializeField] private Transform player;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _blackScreen;

    private Vector2 _playerCanvasPos;
    
    private static readonly int RADIUS = Shader.PropertyToID("_Radius");
    private static readonly int CENTER_X = Shader.PropertyToID("_CenterX");
    private static readonly int CENTER_Y = Shader.PropertyToID("_CenterY");

    public override void ExecuteCustomStartTransition()
    {
        DrawBlackScreen();
        OpenBlackScreen();
    }
    
    public override void ExecuteCustomEndTransition()
    {
        DrawBlackScreen();
        CloseBlackScreen();
    }
    
    public void OpenBlackScreen()
    {
        StartCoroutine(Transition(2, 0, 1, 0));
    }

    public void CloseBlackScreen()
    {
        StartCoroutine(Transition(2, 1, 0.15f,0));
        StartCoroutine(Transition(1, 0.15f, 0,2.5f));
    }

    public void DrawBlackScreen()
    {
        var screenWidth = Screen.width;
        var screenHeight = Screen.height;
        var playerScreenPos = Camera.main.WorldToScreenPoint(player.position);
        
        var canvasRect = _canvas.GetComponent<RectTransform>().rect;
        var canvasWidth = canvasRect.width;
        var canvasHeight = canvasRect.height;

        _playerCanvasPos = new Vector2()
        {
            x = (playerScreenPos.x / screenWidth) * canvasWidth,
            y = (playerScreenPos.y / screenHeight) * canvasHeight,
        };
        
        var squareValue = 0f;
        if (canvasWidth > canvasHeight)
        {
            squareValue = canvasWidth;
            _playerCanvasPos.y += (canvasWidth - canvasHeight) * 0.5f;
        }
        else
        {
            squareValue = canvasHeight;
            _playerCanvasPos.x += (canvasHeight - canvasWidth) * 0.5f;

        }

        _playerCanvasPos /= squareValue;
        var mat = _blackScreen.material;
        
        mat.SetFloat(CENTER_X, _playerCanvasPos.x);
        mat.SetFloat(CENTER_Y, _playerCanvasPos.y);
        _blackScreen.rectTransform.sizeDelta = new Vector2(squareValue, squareValue);
    }

    public IEnumerator Transition(float duration, float beginRadius, float endRadius, float delay)
    {
        yield return new WaitForSeconds(delay);
        var mat = _blackScreen.material;
        var time = 0f;
        while (time <= duration)
        {
            time += Time.deltaTime;
            var t = time / duration;
            var radius = Mathf.Lerp(beginRadius, endRadius, t);
            
            mat.SetFloat(RADIUS, radius); //RADIUS
            yield return null;
        }
    }

}
