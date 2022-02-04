using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FYP.PlayerMovement
{
    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float playerSpeed = 2.0f;
        [SerializeField]
        private float playerSprint = 15.0f;
        [SerializeField]
        private float jumpHeight = 1.0f;
        [SerializeField]
        private float gravityValue = -9.81f;
        [SerializeField]
        private float rotationSpeed = 5f;

        private CharacterController controller;     //refer to character controller
        private PlayerInput playerInput;            //refer to playerinput in editor
        private Vector3 playerVelocity;             //refer to player movement
        private bool groundedPlayer;
        private Transform cameraTransform;          //mouse rotation for cinemachine
        //public bool isSprinting;

        private Animator animator;

        private InputAction moveAction;             //WASD action
        private InputAction jumpAction;             //Jump action
        private InputAction sprintAction;           //Running action

        //private void Awake()
        //{
        //    playerInput = new PlayerInput();

        //    playerInput.actions.Sprint.performed += x => SprintPressed();
        //}

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            playerInput = GetComponent<PlayerInput>();
            cameraTransform = Camera.main.transform;
            moveAction = playerInput.actions["Move"];
            //lookAction = playerInput.actions["Look"];
            jumpAction = playerInput.actions["Jump"];
            sprintAction = playerInput.actions["Sprint"];
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

           Vector2 input = moveAction.ReadValue<Vector2>();
           if (input.magnitude > 0)
            {
                animator.SetBool("isWalking", true);
                Debug.Log("Walking");
                Vector3 move = new Vector3(input.x, 0, input.y);
                move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
                move.y = 0f;
                controller.Move(move * Time.deltaTime * playerSpeed);
            }
            else
            {
                animator.SetBool("isWalking", false);
                Debug.Log("Idle");
            }

            // Changes the height position of the player..
            //if (jumpAction.triggered && groundedPlayer) --grounded player is not detect--
            if (jumpAction.triggered)
            {
                animator.Play("Jumping");
                playerVelocity.y += Mathf.Sqrt(jumpHeight * 1.0f * gravityValue);
                Debug.Log("Jump");
            }

            if (sprintAction.triggered)
            {
                animator.Play("Running");
                Vector3 run = new Vector3(input.x, 0, input.y);
                run = run.x * cameraTransform.right.normalized + run.z * cameraTransform.forward.normalized;
                run.y = 0f;
                controller.Move(run * Time.deltaTime * playerSprint);
                Debug.Log("Running");
            }

            //Vector2 sprint = sprintAction.ReadValue<Vector2>();
            //if(sprint.magnitude > 0)
            //{
            //    animator.SetBool("isRunning", true);
            //    Vector3 run = new Vector3(sprint.x, 0, sprint.y);
            //    run = run.x * cameraTransform.right.normalized + run.z * cameraTransform.forward.normalized;
            //    run.y = 0f;
            //    controller.Move(run * Time.deltaTime * playerSprint);
            //}
            //else
            //{
            //    animator.SetBool("isRunning", false);
            //}




            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            // Rotate towards camera direction        
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
}