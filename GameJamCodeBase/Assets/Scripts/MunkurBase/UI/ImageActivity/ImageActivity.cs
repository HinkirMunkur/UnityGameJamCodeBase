using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public abstract class ImageActivity : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Image image;

    public void DeactivateButton()
    {
        image.raycastTarget = false;
    }

    public void ActivateButton()
    {
        image.raycastTarget = true;
    }

    public abstract void OnPointerClick(PointerEventData eventData);
}
