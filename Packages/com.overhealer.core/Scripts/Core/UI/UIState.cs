using UnityEngine;
using UnityEngine.InputSystem;

namespace overhealer.Core
{
    public class UIState :
            MonoBehaviour
    {
        [SerializeField]
        private InputActionReference closeInputAction;

        public virtual void Enable()
        {
            if (closeInputAction)
                closeInputAction.action.performed += Close;
        }

        public virtual void Disable()
        {
            if (closeInputAction)
                closeInputAction.action.performed -= Close;
        }

        public virtual void Close(InputAction.CallbackContext callbackContext)
        {
        }
    }
}