using UnityEngine.EventSystems;
using UnityEngine;
using Munkur;

public class OptionButtonActivity : ButtonActivity
{
    [SerializeField] private int optionLevelIndex;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        LevelController.Instance.LoadLevelWithIndex(optionLevelIndex);
    }
}
