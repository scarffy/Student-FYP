using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FYP.PlayerMovement
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        public Vector2 forward;

        [SerializeField]
        private float playerSpeed = 20.0f;
        //[SerializeField]
        //private float playerSprint = 15.0f;
        [SerializeField]
        private float jumpHeight = 1.0f;
        [SerializeField]
        private float gravityValue = -9.81f;
        [SerializeField]
        private float rotationSpeed = 5f;

        [SerializeField]
        private float animationSmoothTime = 0.1f;
        [SerializeField]
        private float animationPlayTransition = 0.15f;


        private CharacterController controller;     //refer to character controller
        private PlayerInput playerInput;            //refer to playerinput in editor
        private Vector3 playerVelocity;             //refer to player movement
        private bool groundedPlayer;
        private Transform cameraTransform;          //mouse rotation for cinemachine
        //private Vector2 input;


        private Animator animator;
        int jumpAnimation;
        int attackAnimation;
        int defenceAnimation;
        int sprintAnimation;
        int moveXAnimationParameterId;
        int moveZAnimationParameterId;

        Vector2 currentAnimationBlendVector;
        Vector2 animationVelocity;

        private InputAction moveAction;             //WASD action
        private InputAction jumpAction;             //Jump action
        private InputAction sprintAction;           //Running action
        private InputAction attackAction;           //Attacking Action
        private InputAction defenceAction;           //Attacking Action


        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
            cameraTransform = Camera.main.transform;
            //Cache a reference to all of the input actions to avoid them with strings constantly
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
            sprintAction = playerInput.actions["Sprint"];
            attackAction = playerInput.actions["Attack"];
            defenceAction = playerInput.actions["Defence"];
            //Animatons
            animator = GetComponent<Animator>();
            jumpAnimation = Animator.StringToHash("Jumping");
            attackAnimation = Animator.StringToHash("Attacking");
            defenceAnimation = Animator.StringToHash("Defence");
            sprintAnimation = Animator.StringToHash("isSprinting");
            moveXAnimationParameterId = Animator.StringToHash("MoveX");
            moveZAnimationParameterId = Animator.StringToHash("MoveZ");
            
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }


            Vector2 input = moveAction.ReadValue<Vector2>();
            currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, animationSmoothTime);
            Vector3 move = new Vector3(input.x, 0, input.y);
            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            //Blend Strafe Animation
            animator.SetFloat(moveXAnimationParameterId, currentAnimationBlendVector.x);
            animator.SetFloat(moveZAnimationParameterId, currentAnimationBlendVector.y);

            UpdateIsSprinting();

            if (jumpAction.triggered)
            {
                //playerVelocity.y += Mathf.Sqrt(jumpHeight * 1.0f * gravityValue);
                animator.Play(jumpAnimation);
            }

            if (attackAction.triggered)
                animator.Play(attackAnimation);
            

            if (defenceAction.triggered)
                animator.Play(defenceAnimation);


            //if (sprintAction.triggered)
            //{
            //    animator.SetBool("isSprinting", true);
            //    Debug.Log("run");
            //}
            //else
            //{
            //    animator.SetBool("isSprinting", false);
            //    Debug.Log("frame");
            //}


            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            // Rotate towards camera direction        
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }

        private void UpdateIsSprinting()
        {
            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            animator.SetBool(sprintAnimation, isSprinting);
        }

    }
}