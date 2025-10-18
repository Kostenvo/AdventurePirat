using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Subscribe.Extensions
{
    public static class UnitySubscribeExtension
    {
        public static ActionDisposable SubsctibeDisposable(this UnityEvent enent, UnityAction forSubscribe)
        {
            enent.AddListener(forSubscribe);
            return new ActionDisposable(() => enent.RemoveListener(forSubscribe));
        }

        public static ActionDisposable SubsctibeDisposable<T>(this UnityEvent<T> enent, UnityAction<T> forSubscribe)
        {
            enent.AddListener(forSubscribe);
            return new ActionDisposable(() => enent.RemoveListener(forSubscribe));
        }

        public static void SubscribeInputPreformed(this ComposideDisposible trash, InputAction inputAction, 
            Action<InputAction.CallbackContext> callback)
        {
            inputAction.performed += callback;
            trash.Retain(new ActionDisposable(() => inputAction.performed -= callback));
        }
        
        public static void SubscribeInputStarted(this ComposideDisposible trash, InputAction inputAction, 
            Action<InputAction.CallbackContext> callback)
        {
            inputAction.started += callback;
            trash.Retain(new ActionDisposable(() => inputAction.started -= callback));
        }
        
        public static void SubscribeInputCanceled(this ComposideDisposible trash, InputAction inputAction, 
            Action<InputAction.CallbackContext> callback)
        {
            inputAction.canceled += callback;
            trash.Retain(new ActionDisposable(() => inputAction.canceled -= callback));
        }
    }
}