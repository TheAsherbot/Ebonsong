using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerNameSpace
{
    public class Player : MonoBehaviour
    {
        public PlayerInputActions InputActions
        {
            get; 
            private set;
        }
        public bool IsMale
        {
            get;
            private set;
        }
        
        [SerializeField] private GameObject maleBase;
        [SerializeField] private GameObject femaleBase;
        

        private void Awake()
        {
            IsMale = true;
            femaleBase.SetActive(false);
            InputActions = new PlayerInputActions();
            InputActions.AllTerain.Enable();
            InputActions.LandMovement.Enable();
            InputActions.AllTerain.ChangeForm.performed += ChangeForm;

        }

        private void ChangeForm(InputAction.CallbackContext context)
        {
            if (IsMale == true)
            {
                IsMale = !IsMale;
                maleBase.SetActive(false);
                femaleBase.SetActive(true);
            }
            else
            {
                IsMale = !IsMale;
                maleBase.SetActive(true);
                femaleBase.SetActive(false);
            }
        }

    }
}
