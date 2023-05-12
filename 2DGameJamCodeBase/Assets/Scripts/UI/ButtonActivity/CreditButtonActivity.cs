using UnityEngine.EventSystems;
using UnityEngine;
using Munkur;

public class CreditButtonActivity : ButtonActivity
{
    [SerializeField] private int creditLevelIndex;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        LevelController.Instance.LoadLevelWithIndex(creditLevelIndex);
    }
}
