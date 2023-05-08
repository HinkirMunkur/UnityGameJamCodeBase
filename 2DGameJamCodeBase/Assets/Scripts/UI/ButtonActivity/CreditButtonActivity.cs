using UnityEngine;
using UnityEngine.EventSystems;

public class CreditButtonActivity : ButtonActivity
{
    [SerializeField] private int creditLevelIndex;
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        LevelController.Instance.LoadLevelWithIndex(creditLevelIndex);
    }
}
