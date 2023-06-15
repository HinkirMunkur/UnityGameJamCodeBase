using UnityEngine;
using System;

namespace Munkur
{
    public class InputSystemManager : SingletonnPersistent<InputSystemManager>
    {
        public Action<Vector2> OnInputStarted;
        public Action<Vector2> OnInputContinued;
        public Action<Vector2> OnInputFinished;
    
        [SerializeField] private bool enableInputListener = false;
    
        public bool EnableInputListener => enableInputListener;

        [SerializeField] private bool isMobileInput;

        private void Update()
        {
            if (enableInputListener)
            {
                if (isMobileInput)
                {
                    HandleTouchInput();
                }
                else
                {
                    HandleMouseInput();
                }
            }
        }

        private void HandleTouchInput()
        {
            if(Input.touchCount > 0)
            {
                var theTouch = Input.GetTouch(0);

                if (theTouch.phase == TouchPhase.Began)
                {
                    OnInputStarted?.Invoke(theTouch.deltaPosition);
                }

                if (theTouch.phase == TouchPhase.Moved)
                {
                    OnInputContinued?.Invoke(theTouch.deltaPosition);   
                }

                if (theTouch.phase == TouchPhase.Ended)
                {
                    OnInputFinished?.Invoke(theTouch.deltaPosition);
                }
            }
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnInputStarted?.Invoke(Input.mousePosition);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                OnInputFinished?.Invoke(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                OnInputContinued?.Invoke(Input.mousePosition);
            }
        }
    }
}