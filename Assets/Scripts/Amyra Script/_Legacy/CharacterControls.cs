//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//namespace FYP.legacy
//{
//    [RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
//    public class CharacterControls : MonoBehaviour
//    {
//        [SerializeField]
//        private float playerSpeed = 2.0f;
//        [SerializeField]
//        private float playerSprint = 15.0f;
//        [SerializeField]
//        private float jumpHeight = 1.0f;
//        [SerializeField]
//        private float gravityValue = -9.81f;
//        [SerializeField]
//        private float rotationSpeed = 5f;

//        private CharacterController controller;     //refer to character controller
//        private PlayerInput playerInput;            //refer to playerinput in editor
//        private Vector3 playerVelocity;             //refer to player movement
//        private bool groundedPlayer;
//        private Transform cameraTransform;          //mouse rotation for cinemachine
//        private Vector2 input;


//        private Animator animator;

//        private InputAction moveAction;             //WASD action
//        private InputAction sprintAction;           //Running action
//        private InputAction talkAction;           //Attacking Action

//        private void Start()
//        {
//            controller = GetComponent<CharacterController>();
//            animator = GetComponent<Animator>();
//            playerInput = GetComponent<PlayerInput>();
//            cameraTransform = Camera.main.transform;
//            moveAction = playerInput.actions["Moving"];
//            sprintAction = playerInput.actions["Sprinting"];
//            talkAction = playerInput.actions["Talking"];
//        }

//        private void Update()
//        {
//            input = moveAction.ReadValue<Vector2>();
//            if (input.magnitude > 0)
//            {
//                animator.SetBool("isWalking", true);
//                Debug.Log("Walking");
//                Vector3 move = new Vector3(input.x, 0, input.y);
//                move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
//                move.y = 0f;
//                controller.Move(move * Time.deltaTime * playerSpeed);
//            }
//            else
//            {
//                animator.SetBool("isWalking", false);
//                Debug.Log("Idle");
//            }

//            if (sprintAction.triggered)
//            {
//                animator.Play("Running");
//                Vector3 run = new Vector3(input.x, 0, input.y);
//                run = run.x * cameraTransform.right.normalized + run.z * cameraTransform.forward.normalized;
//                run.y = 0f;
//                controller.Move(run * Time.deltaTime * playerSprint);
//                Debug.Log("Running");
//            }



//        }

//    }
//}
