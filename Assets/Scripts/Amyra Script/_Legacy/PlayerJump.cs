using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FYP.legacy
{
    public class PlayerJump : MonoBehaviour
    {
        private PlayerAction playerAction;
        private InputAction movement;

        private void Awake()
        {
            playerAction = new PlayerAction();
        }

        private void OnEnable()
        {
            movement = playerAction.Player.Move;
            movement.Enable();

            playerAction.Player.Jump.performed += DoJump;
            playerAction.Player.Jump.Enable();
        }

        private void OnDisable()
        {
            movement.Disable();
            playerAction.Player.Jump.Disable();
        }

        private void FixedUpdate()
        {
            Debug.Log("Movement Values" + movement.ReadValue<Vector3>());
        }

        private void DoJump(InputAction.CallbackContext obj)
        {
            Debug.Log("Jumping");
        }

    }
}
