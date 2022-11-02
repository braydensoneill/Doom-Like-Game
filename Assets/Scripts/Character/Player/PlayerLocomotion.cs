using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJM
{
    public class PlayerLocomotion : MonoBehaviour
    {
        private Transform cameraObject;
        private InputHandler inputHandler;
        private Vector3 moveDirection;

        [HideInInspector] public Transform myTransform;
        [HideInInspector] public PlayerAnimatorHandler playerAnimatorHandler;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField] float movementSpeed = 6.5f;
        [SerializeField] float rotationSpeed = 10;

        // Movement variables
        private Vector3 normalVector;
        private Vector3 targetPosition;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            playerAnimatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            playerAnimatorHandler.Initialise();
        }

        public void Update()
        {
            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);

            HandleMovement(delta);
        }

        #region Movement
        private void HandleRotation(float _delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = inputHandler.moveAmount;

            targetDir = cameraObject.forward * inputHandler.vertical;
            targetDir += cameraObject.right * inputHandler.horizontal;

            targetDir.Normalize();
            targetDir.y = 0;

            if (targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            float rs = rotationSpeed;

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * _delta);

            myTransform.rotation = targetRotation;
        }

        private void HandleMovement(float _delta)
        {
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;
            moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            playerAnimatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

            if (playerAnimatorHandler.canRotate)
            {
                HandleRotation(_delta);
            }
        }
        #endregion
    }
}