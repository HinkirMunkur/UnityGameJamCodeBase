using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class StaticUtilitiesBase : MonoBehaviour
{
    public static bool IsPointerOverUIObject()
    {
        if (EventSystem.current == null)
        {
            return false;
        }
        
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
