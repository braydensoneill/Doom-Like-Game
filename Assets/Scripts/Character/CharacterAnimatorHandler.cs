using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJM
{
    public class CharacterAnimatorHandler : MonoBehaviour
    {
        public Animator animator;

        public void PlayTargetAnimation(string _targetAnimation, bool isInteracting)
        {
            animator.applyRootMotion = isInteracting;
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(_targetAnimation, 0.2f);
        }
    }
}
