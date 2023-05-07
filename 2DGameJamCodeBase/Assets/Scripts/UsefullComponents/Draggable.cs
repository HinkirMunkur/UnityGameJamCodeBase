using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector2 startPosition;

    private void Awake()
    {
        MouseInputSystemManager.Instance.OnMouseLeftClicked += OnDragStart;    
        MouseInputSystemManager.Instance.OnMouseDragged += OnDrag;    
        MouseInputSystemManager.Instance.OnMouseReleased += OnDragEnd;    
    }

    private void OnDestroy() 
    {
        MouseInputSystemManager.Instance.OnMouseLeftClicked -= OnDragStart;    
        MouseInputSystemManager.Instance.OnMouseDragged -= OnDrag;    
        MouseInputSystemManager.Instance.OnMouseReleased -= OnDragEnd;  
    }

    public virtual void OnDragStart(Vector2 delta) 
    {
        startPosition = delta;
    }

    public virtual void OnDrag(Vector2 delta) 
    {
        transform.position = delta;
    }

    public virtual void OnDragEnd(Vector2 delta) 
    {

    }
}
