using UnityEngine.EventSystems;
using UnityEngine;
using Munkur;

public class StartButtonActivity : ButtonActivity
{
    [SerializeField] private int firstLevelIndex;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        LevelController.Instance.LoadLevelWithIndex(firstLevelIndex);
    }
}
