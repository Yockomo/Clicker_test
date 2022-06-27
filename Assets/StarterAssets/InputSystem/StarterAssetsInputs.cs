using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 touch;


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnTouch(InputValue value)
		{
			TouchInput(value.Get<Vector2>());
		}

#endif
		public void TouchInput(Vector2 newTouchDirection)
		{
			touch = newTouchDirection;
		} 
	}
	
}