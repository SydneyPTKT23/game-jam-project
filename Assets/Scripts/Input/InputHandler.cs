using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SLC.Input
{
    public class InputHandler : MonoBehaviour, PlayerControls.IPlayerInputActions
    {
        public Vector2 MouseDelta { get; private set; }
        public Vector2 InputVector { get; private set; }


        public PlayerControls InputActions { get; private set; }

        private void OnEnable()
        {
            if (InputActions != null)
                return;

            InputActions = new PlayerControls();
            InputActions.PlayerInput.SetCallbacks(this);
            InputActions.PlayerInput.Enable();
        }

        private void OnDisable()
        {
            InputActions.PlayerInput.Disable();
        }

        public void OnPrimaryAction(InputAction.CallbackContext t_context)
        {

        }

        public void OnJump(InputAction.CallbackContext t_context)
        {

        }

        public void OnMove(InputAction.CallbackContext t_context)
            => InputVector = t_context.ReadValue<Vector2>();

        public void OnLook(InputAction.CallbackContext t_context)
            => MouseDelta = t_context.ReadValue<Vector2>();

        public void DisableActionFor(InputAction t_action, float t_seconds)
        {
            StartCoroutine(DisableAction(t_action, t_seconds));
        }

        private IEnumerator DisableAction(InputAction t_action, float t_seconds)
        {
            t_action.Disable();

            yield return new WaitForSeconds(t_seconds);

            t_action.Enable();
        }
    }
}