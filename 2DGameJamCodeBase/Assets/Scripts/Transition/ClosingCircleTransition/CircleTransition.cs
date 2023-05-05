using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CircleTransition : Transition
{
    [SerializeField] private Canvas _canvas;
    
    [Header("Tracked Object Transform")]
    [SerializeField] private Transform player = null;

    private Vector2 _playerCanvasPos;
    
    private static readonly int RADIUS = Shader.PropertyToID("_Radius");
    private static readonly int CENTER_X = Shader.PropertyToID("_CenterX");
    private static readonly int CENTER_Y = Shader.PropertyToID("_CenterY");

    public override void ExecuteCustomStartTransition(float duration)
    {
        blackBackground.enabled = true;
        
        DrawBlackScreen();
        OpenBlackScreen(duration);
    }
    
    public override void ExecuteCustomEndTransition(float duration)
    {
        blackBackground.enabled = true;
        
        DrawBlackScreen();
        CloseBlackScreen(duration);
    }
    
    private void OpenBlackScreen(float duration)
    {
        StartCoroutine(Transition(duration, 0, 1, 0));
    }

    private void CloseBlackScreen(float duration)
    {
        StartCoroutine(Transition(duration, 1, 0.15f,0));
        StartCoroutine(Transition(duration/2, 0.15f, 0,2.5f));
    }

    private void DrawBlackScreen()
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
        var mat = blackBackground.material;
        
        mat.SetFloat(CENTER_X, _playerCanvasPos.x);
        mat.SetFloat(CENTER_Y, _playerCanvasPos.y);
        blackBackground.rectTransform.sizeDelta = new Vector2(squareValue, squareValue);
    }

    private IEnumerator Transition(float duration, float beginRadius, float endRadius, float delay)
    {
        yield return new WaitForSeconds(delay);
        var mat = blackBackground.material;
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
    
#if UNITY_EDITOR
    public override void SetTransitionReferences(Image blackBG)
    {
        base.SetTransitionReferences(blackBG);
        _canvas = blackBG.GetComponentInParent<Canvas>();
        
        String name = "CircleTransitionCenterPoint";

        GameObject createdObj = GameObject.Find(name);
        
        if (createdObj != null)
        {
            player = createdObj.transform;
            return;
        }

        GameObject GO = new GameObject();
        GO.transform.position = Vector3.zero;

        GO.name = name;

        player = GO.transform;
    }
    
#endif

}
