using UnityEngine;
using System;
using Munkur;

[RequireComponent(typeof(Collider2D))]
public class Clickable : MonoBehaviour
{
    [SerializeField] private Camera rayCamera;
    [SerializeField] private LayerMask layer;

    [SerializeField] private bool oneTime;

    [SerializeField] private bool considerUI;
    
    public bool OneTime => oneTime;
    
    public Action OnClicked;

    private RaycastHit2D raycastHit2D;
    private Ray ray;
    
    private void Awake()
    { 
        InputSystemManager.Instance.OnInputStarted += OnClick;
    }

    private void OnDestroy() 
    {
        InputSystemManager.Instance.OnInputStarted -= OnClick;    
    }

    public virtual void OnClick(Vector2 mousePosition) 
    {
        ray = rayCamera.ScreenPointToRay(Input.mousePosition);
        raycastHit2D = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layer);
        
        if (raycastHit2D.collider != null && raycastHit2D.collider.gameObject == gameObject)
        {
            if (considerUI && StaticUtilitiesBase.IsPointerOverUIObject())
            {
                return;
            }
            
            OnClicked?.Invoke();

            if (oneTime)
            {
                InputSystemManager.Instance.OnInputStarted -= OnClick; 
            }
        }
    }
}
