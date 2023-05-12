using UnityEngine;
using System;

public class Clickable : MonoBehaviour
{
    [SerializeField] private Camera rayCamera;
    [SerializeField] private LayerMask layer;
    
    public Action OnClicked;

    private RaycastHit2D ray;
    private void Start()
    { 
        MouseInputSystemManager.Instance.OnMouseLeftClicked += OnClick;
    }

    private void OnDestroy() 
    {
        MouseInputSystemManager.Instance.OnMouseLeftClicked -= OnClick;    
    }

    public virtual void OnClick(Vector2 mousePosition) 
    {
        ray = Physics2D.Raycast(rayCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 
            Mathf.Infinity, layer);

        if (ray.collider != null && ray.collider.gameObject == gameObject) 
        {
            OnClicked?.Invoke();
        }
    }
}
