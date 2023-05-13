using UnityEngine.EventSystems;
using UnityEngine;
using Munkur;

public class ExitButtonActivity : ButtonActivity
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}
