using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BJM
{
    public class PlayerManager : CharacterManager
    {
        private InputHandler inputHandler;
        private Animator animator;

        // Start is called before the first frame update
        void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            inputHandler.isInteracting = animator.GetBool("isInteracting");
        }
    }
}
