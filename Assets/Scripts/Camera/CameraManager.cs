using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJM
{
    public class CameraManager : MonoBehaviour
    {
        [Header("General")]
        public Transform targetTransform;
        public Transform cameraTransform;
        public Transform cameraPivotTransform;
        private Transform myTransform;
        private Vector3 cameraTransformPosition;
        private LayerMask ignoreLayers;

        public static CameraManager singleton;

        public float lookSpeed = 0.1f;
        public float followSpeed = 0.1f;
        public float pivotSpeed = 0.03f;

        private float defaultPosition;
        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -35;
        public float maximumPivot = 35;

        private void Awake()
        {
            singleton = this;
            myTransform = transform;
            defaultPosition = cameraTransform.localPosition.z;
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }
        
        public void FollowTarget(float _delta)
        {
            Vector3 targetPosition = Vector3.Lerp(
                myTransform.position, 
                targetTransform.position, 
                _delta / followSpeed);
            myTransform.position = targetPosition;
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
    }
}