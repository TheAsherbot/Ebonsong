using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

namespace PlayerNameSpace
{
    public class Player : MonoBehaviour
    {
        public bool isLookingRight;

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
        [SerializeField] private GameObject maleSword;
        [SerializeField] private GameObject femaleSword;

        private GameObject rightArm;

        private void Awake()
        {
            IsMale = true;
            femaleBase.SetActive(false);
            rightArm = maleSword.transform.parent.gameObject;
            InputActions = new PlayerInputActions();
            InputActions.AllTerain.Enable();
            InputActions.LandMovement.Enable();
            InputActions.AllTerain.ChangeForm.performed += ChangeForm;
        }

        public void SetSwordActive()
        {
            rightArm.SetActive(true);
            maleSword.SetActive(IsMale);
            femaleSword.SetActive(!IsMale);
        }
        
        public void SetSwordInactive()
        {
            rightArm.SetActive(false);
        }

        private void ChangeForm(InputAction.CallbackContext context)
        {
            if (IsMale == true)
            {
                ChangeToFemale();
            }
            else
            {
                ChangeToMale();
            }
        }

        private void ChangeToFemale()
        {
            IsMale = false;
            maleBase.SetActive(IsMale);
            femaleBase.SetActive(!IsMale);
            maleSword.SetActive(IsMale);
            femaleSword.SetActive(!IsMale);
        }

        private void ChangeToMale()
        {
            IsMale = true;
            maleBase.SetActive(IsMale);
            femaleBase.SetActive(!IsMale);
            maleSword.SetActive(IsMale);
            femaleSword.SetActive(!IsMale);
        }

    }
}
