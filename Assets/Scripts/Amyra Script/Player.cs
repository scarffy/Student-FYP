using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private PlayerAction playerAction;
    //private Rigidbody rigidBody;
    private CharacterController characterController;
    private Vector3 movementInput;

    void Awake()
    {
        playerAction = new PlayerAction();

        characterController = GetComponent<CharacterController>();

        //rigidBody = GetComponent<Rigidbody>();
        //if (rigidBody is null)
        //    Debug.LogError("Error!");
    }

    private void OnEnable()
    {
        playerAction.Player.Enable();
    }

    private void OnDisable()
    {
        playerAction.Player.Disable();
    }

    void FixedUpdate()
    {
        movementInput = playerAction.Player.Movement.ReadValue<Vector3>();
        //rigidBody.velocity = movementInput * speed;

        characterController.Move(movementInput * Time.deltaTime);
    }
}
