

using System;
using UnityEngine.InputSystem;

namespace WizardsAndGoblins
{
    public interface IInputManager
    {
        event Action<InputAction.CallbackContext> InputActionPerformed;
        event Action<InputAction.CallbackContext> InputActionStarted;
        event Action<InputAction.CallbackContext> InputActionCanceled;
        
        void SubscribeToInputActionsEvents();
        void UnsubscribeToInputActionsEvents();

        void OnInputActionPerformed(InputAction.CallbackContext context);
        void OnInputActionStarted(InputAction.CallbackContext context);
        void OnInputActionCanceled(InputAction.CallbackContext context);

    }
}
