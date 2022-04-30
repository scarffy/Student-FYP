using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		public PhotonPlayerController playerController;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool attack;
		public bool defence;
		public bool interacted;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

		public bool isUiOn = false;

		public bool IsUiOn
        {
			get { return isUiOn; }
			set { 
				isUiOn = value;
				cursorLocked = !IsUiOn;
				SetCursorState(!IsUiOn);
				FYP.UI.UIStateManager.Instance.SetState(FYP.UI.UIStateManager.State.quit);
			}
        }

        void Start()
        {
			SetCursorState(false);
		}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnOpenUI(InputValue value)
        {
			//IsUiOn = !IsUiOn;
        }

		public void OnMove(InputValue value)
		{
			if (!isUiOn)
				MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook && !isUiOn)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			if(!isUiOn)
				JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			if(!isUiOn)
				SprintInput(value.isPressed);
		}

		public void OnAttack(InputValue value)
        {
			if(!isUiOn)
				attack = value.isPressed;
        }

		public void OnDefence(InputValue value)
		{
			if(!isUiOn)
				defence = value.isPressed;
		}

		public void OnInteraction(InputValue value)
        {
			InteractInput(value.isPressed);
        }

#else
	// old input sys if we do decide to have it (most likely wont)...
#endif

        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AttackInput(bool newAttackState)
        {
			attack = newAttackState;
        }

		public void InteractInput(bool newInteracState)
        {
			interacted = newInteracState;
        }

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			//SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}