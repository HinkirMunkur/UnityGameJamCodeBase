using UnityEngine;
using System;

public class Clickable : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask layer;
    public Action OnClicked;

    private RaycastHit2D ray;
    private void Awake()
    { 
        MouseInputSystemManager.Instance.OnMouseLeftClicked += OnClick;    
 
    }

    private void OnDestroy() 
    {
        MouseInputSystemManager.Instance.OnMouseLeftClicked -= OnClick;    
    }

    public virtual void OnClick(Vector2 mousePosition) 
    {
        ray = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, layer);

        if (ray.collider != null) 
        {
            OnClicked?.Invoke();
        }
    }
}
