using System;
using UnityEngine;

namespace Munkur
{
    public sealed class MouseInputSystemManager : SingletonnPersistent<MouseInputSystemManager>
    {
        public Action<Vector2> OnMouseLeftClicked;
        //public Action<Vector2> OnMouseRightClicked;
        //public Action<Vector2> OnMouseScrollClicked;
        public Action<Vector2> OnMouseDragged;
        public Action<Vector2> OnMouseReleased;
    
        [SerializeField] private bool enableInputListener = false;
    
        public bool EnableInputListener => enableInputListener;

        private void Update()
        {
            if(enableInputListener)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnMouseLeftClicked?.Invoke(Input.mousePosition);
                }
                else if(Input.GetMouseButtonUp(0))
                {
                    OnMouseReleased?.Invoke(Input.mousePosition);
                }
                else if (Input.GetMouseButton(0))
                {
                    OnMouseDragged?.Invoke(Input.mousePosition);
                }
            }
        }
    }
}
