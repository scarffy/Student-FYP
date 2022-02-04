using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float gravityValue;

    private PlayerAction playerAction;
    private CharacterController characterController;
    private Vector3 movementInput;
    private InputAction jump;
    private bool groundedPlayer;
    private Transform cameraMainTransform;

    void Awake()
    {
        playerAction = new PlayerAction();

        characterController = GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;               
    }

    private void OnEnable()
    {
        playerAction.Player.Enable();

        //trigged by events
        playerAction.Player.Jump.performed += DoJump;
        playerAction.Player.Jump.Enable();
    }

    private void OnDisable()
    {
        playerAction.Player.Disable();
        playerAction.Player.Jump.Disable();
    }

    void FixedUpdate()
    {
        //!This is for player movement
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && movementInput.y < 0)
        {
            movementInput.y = 0f;
        }

        //movementInput = playerAction.Player.Movement.ReadValue<Vector3>();
        
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        //move.y = 0f;
        characterController.Move(movementInput * Time.deltaTime);

        movementInput.y += gravityValue * Time.deltaTime;

    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (groundedPlayer)
        {
            movementInput.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            Debug.Log("Jump");
        }
        

    }
}
