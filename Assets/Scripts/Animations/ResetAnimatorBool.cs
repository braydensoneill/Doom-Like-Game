using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJME
{
    public class ResetAnimatorBool : StateMachineBehaviour
    {
        public string targetBool;
        public bool status;

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(targetBool, status);
        }
    }
}
