using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace Munkur
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonActivity : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Button button;

        public void DeactivateButton()
        {
            button.interactable = false;
        }

        public void ActivateButton()
        {
            button.interactable = true;
        }

        public abstract void OnPointerClick(PointerEventData eventData);
    
#if UNITY_EDITOR

        protected void SetProperButton()
        {
            button = GetComponent<Button>();
        }
    
#endif
    
    }
}

