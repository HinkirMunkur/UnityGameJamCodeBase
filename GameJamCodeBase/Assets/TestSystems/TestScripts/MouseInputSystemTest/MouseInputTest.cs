using UnityEngine;

public class MouseInputTest : MonoBehaviour
{
    [SerializeField] private Draggable _draggableObject;
    [SerializeField] private Clickable _clickableObject;

    private void Start() 
    {
        _draggableObject.OnDragFinished += DragFinished;
        _clickableObject.OnClicked += Clicked;
    }

    private void OnDestroy() 
    {
        _draggableObject.OnDragFinished -= DragFinished;
        _clickableObject.OnClicked -= Clicked;    
    }

    private void DragFinished() 
    {
        Debug.Log("DRAG ENDED");
    }

    private void Clicked() 
    {
        Debug.Log("CLICKED");
    }
}
