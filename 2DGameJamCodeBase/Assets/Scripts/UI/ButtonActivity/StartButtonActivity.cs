using UnityEngine;
using UnityEngine.EventSystems;

public class StartButtonActivity : ButtonActivity
{
    [SerializeField] private int firstLevelIndex;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        LevelController.Instance.LoadLevelWithIndex(firstLevelIndex);
    }
}
