using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 currentPosition;
    private bool isBeingDragged = false;

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
        RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

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
    }
}
