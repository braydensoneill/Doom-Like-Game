using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJM
{
    public class InputManager : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        PlayerControls inputActions;
        CameraManager cameraManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            cameraManager = FindObjectOfType<CameraManager>();
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
