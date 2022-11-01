using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJM
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        PlayerControls inputActions;
        CameraHandler cameraManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            cameraManager = FindObjectOfType<CameraHandler>();
        }

        public void OnEnable()
        {
            if(inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float _delta)
        {
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

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if(cameraManager != null)
            {
                cameraManager.FollowTarget(delta);
                cameraManager.HandleCameraRotation(delta, mouseX, mouseY);
            }
        }
    }
}
