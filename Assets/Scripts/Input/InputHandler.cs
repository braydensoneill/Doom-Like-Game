using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJM
{
    public class InputHandler : MonoBehaviour
    {
        private PlayerControls inputActions;

        [Header("Inputs")]
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;
        public float input_crouch;
        private Vector2 movementInput;
        private Vector2 cameraInput;

        public void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerControls();

                #region Handle Keybinds
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                #endregion 
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float _delta)
        {
            // All input functions go here
            MoveInput(_delta);
        }

        private void MoveInput(float _delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
    }
}
