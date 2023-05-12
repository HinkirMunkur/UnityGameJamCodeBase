using UnityEngine;
using System;
using Munkur;

public class Draggable : MonoBehaviour
{
    [SerializeField] private Camera rayCamera;
    [SerializeField] private LayerMask layer;

    public Action OnDragFinished;
    
    private Vector3 currentPosition;
    private bool isBeingDragged = false;
    private RaycastHit2D ray;

    private void Awake()
    { 
        MouseInputSystemManager.Instance.OnMouseDragged += OnDrag;    
        MouseInputSystemManager.Instance.OnMouseReleased += OnDragEnd;    
    }

    private void OnDestroy() 
    {
        MouseInputSystemManager.Instance.OnMouseDragged -= OnDrag;    
        MouseInputSystemManager.Instance.OnMouseReleased -= OnDragEnd;  
    }

    public virtual void OnDrag(Vector2 mousePosition) 
    {
        ray = Physics2D.Raycast(rayCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 
            Mathf.Infinity, layer);

        if (ray.collider != null || isBeingDragged) 
        {
            isBeingDragged = true;
            currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0;
            transform.position = currentPosition;
        }
    }

    public virtual void OnDragEnd(Vector2 mousePosition) 
    {
        isBeingDragged = false;
        if (ray.collider != null && ray.collider.gameObject == gameObject) 
        {
            OnDragFinished?.Invoke();
        }
    }
}
