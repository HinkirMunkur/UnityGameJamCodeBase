using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButtonActivity : ButtonActivity
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}
