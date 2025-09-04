using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WizardsAndGoblins.Gameplay
{
    public class PlayerInputManager : Manager, IInputManager
    {
        [SerializeField] private InputActionAsset playerInputAction;

        public event Action<InputAction.CallbackContext> InputActionPerformed;
        public event Action<InputAction.CallbackContext> InputActionStarted;
        public event Action<InputAction.CallbackContext> InputActionCanceled;
        
        public override void Setup()
        {
            base.Setup();
            playerInputAction.Enable();
            SubscribeToInputActionsEvents();
        }

        public override void Dispose()
        {
            base.Dispose();
            playerInputAction.Disable();
            UnsubscribeToInputActionsEvents();
        }

        public void SubscribeToInputActionsEvents()
        {
            foreach (var inputAction in playerInputAction)
            {
                inputAction.performed += OnInputActionPerformed;
                inputAction.started += OnInputActionStarted;
                inputAction.canceled += OnInputActionCanceled; 
            }
        }
        
        public void UnsubscribeToInputActionsEvents()
        {
            foreach (var inputAction in playerInputAction)
            {
                inputAction.performed -= OnInputActionPerformed;
                inputAction.started -= OnInputActionStarted;
                inputAction.canceled -= OnInputActionCanceled; 
            }
        }
        
        public void OnInputActionPerformed(InputAction.CallbackContext context)
        {
            InputActionPerformed?.Invoke(context);
        }

        public void OnInputActionStarted(InputAction.CallbackContext context)
        {
            InputActionStarted?.Invoke(context);
        }

        public void OnInputActionCanceled(InputAction.CallbackContext context)
        {
            InputActionCanceled?.Invoke(context);
        }
    }
}
