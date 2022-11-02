using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJM
{
    public class PlayerAnimatorHandler : CharacterAnimatorHandler
    {
        private InputHandler inputHandler;
        private PlayerLocomotion playerLocomotion;

        int vertical;
        int horizontal;
        public bool canRotate;

        public void Initialise()
        {
            animator = GetComponent<Animator>();
            inputHandler = GetComponentInParent<InputHandler>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float _verticalMovement, float _horizontalMovement)
        {
            #region Vertical
            float v = 0;

            if (_verticalMovement > 0 && _verticalMovement < 0.55f)
                v = 0.5f;

            else if (_verticalMovement > 0.55f)
                v = 1;

            else if (_verticalMovement < 0 && _verticalMovement > -0.55f)
                v = -0.5f;

            else if (_verticalMovement < -0.55f)
                v = -1;

            else
                v = 0;
            #endregion

            #region Horizontal
            float h = 0;

            if (_horizontalMovement > 0 && _horizontalMovement < 0.55f)
                h = 0.5f;

            else if (_horizontalMovement > 0.55f)
                h = 1;

            else if (_horizontalMovement < 0 && _horizontalMovement > -0.55f)
                h = -0.5f;

            else if (_horizontalMovement < -0.55f)
                h = -1;

            else
                h = 0;
            #endregion

            animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = false;
        }

        // Re-adjust the player's model to the center of the game object after performing an animation
        private void OnAnimatorMove()
        {
            if (inputHandler.isInteracting == false)
                return;

            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
            Vector3 deltaPosition = animator.deltaPosition;
            deltaPosition.y = 0; // Fail safe
            Vector3 veolicty = deltaPosition / delta;
            playerLocomotion.rigidbody.velocity = veolicty;
        }
    }
}