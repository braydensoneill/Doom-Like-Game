using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJM
{
    public class CameraHandler : MonoBehaviour
    {
        [Header("General")]
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;

        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        private LayerMask ignoreLayers;
        private Vector3 cameraFollowVelocity = Vector3.zero;

        public static CameraHandler singleton;

        [Header("Camera Movement")]
        public float lookSpeed = 0.015f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.01f;

        private float targetPosition;
        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;

        [Header("Camera Pivot")]
        public float minimumPivot = -35;
        public float maximumPivot = 35;

        [Header("Camera Collisions")]
        public float cameraSphereRadius = 0.2f;
        public float cameraCollisionOffset = 0.2f;
        public float minimumCollisionOffset = 0.2f;

        private void Awake()
        {
            singleton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10 | 1 << 12);
        }
        
        public void FollowTarget(float _delta)
        {
            // Originally used Lerp, now using Smoothdamp because it is much smoother
            Vector3 targetPosition = Vector3.SmoothDamp(
                myTransform.position,
                targetTransform.position,
                ref cameraFollowVelocity, _delta / followSpeed);
            
            // Set the position of the camera to the target position
            myTransform.position = targetPosition;

            // Check for collisions between the camera and appropriate colliders
            HandleCameraCollisions(_delta);
        }

        public void HandleCameraRotation(float _delta, float _mouseXInput, float _mouseInputY)
        {
            lookAngle += (_mouseXInput * lookSpeed) / _delta;
            pivotAngle -= (_mouseInputY * pivotSpeed) / _delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }
    
        public void HandleCameraCollisions(float _delta)
        {
            targetPosition = defaultPosition;
            RaycastHit hit;
            Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
            direction.Normalize();

            // Sphere around the camera, if collision with colliders in game, returns true
            if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition), ignoreLayers))
            {
                // Distance between the camera pivot's transform and the hit point of the sphere cast
                float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);

                // Use the above distance calculation to calculate a new position of the camera after the collision takes place
                targetPosition = -(dis - cameraCollisionOffset);
            }

            if(Mathf.Abs(targetPosition) < minimumCollisionOffset)
            {
                targetPosition = -minimumCollisionOffset;
            }

            cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, _delta / 0.2f);
            cameraTransform.localPosition = cameraTransformPosition;
        }
    }
}