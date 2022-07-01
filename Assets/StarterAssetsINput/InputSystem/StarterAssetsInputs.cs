using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		public event Action<Vector2> OnStartTouch;
		public event Action<Vector2> OnEndTouch;

		private TouchControl touchControl;

        private void Awake()
        {
			touchControl = new TouchControl();
		}

        private void OnEnable()
        {
            touchControl?.Enable();
        }

        private void OnDisable()
        {
            touchControl?.Disable();
        }

		private void Start()
		{
			touchControl.Player.Touch.started += context => StartTouch(context); 
			touchControl.Player.Touch.canceled += context => EndTouch(context); 
		}

		private void StartTouch(InputAction.CallbackContext context)
        {
			OnStartTouch?.Invoke(touchControl.Player.TouchPosition.ReadValue<Vector2>());
        }

		private void EndTouch(InputAction.CallbackContext context)
		{
			OnEndTouch?.Invoke(touchControl.Player.TouchPosition.ReadValue<Vector2>());
		}
	}
	
}