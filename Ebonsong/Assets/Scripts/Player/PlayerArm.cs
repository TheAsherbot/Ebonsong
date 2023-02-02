using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerNameSpace
{
    public class PlayerArm : MonoBehaviour
    {
        [SerializeField] float rotaionOfset;
        [SerializeField] float speed;
        [SerializeField] Transform mousePosition;

        private Player player;

        private void Start()
        {
            player = transform.parent.GetComponent<Player>();
        }

        private void Update()
        {
            transform.right = GetMousePosition2D() - transform.position;
            if (player.isLookingRight == false)
            {
                transform.right = -transform.right;
            }
        }

        public Vector3 GetMousePosition2D()
        {
            Vector2 stick = player.InputActions.LandMovement.Sword.ReadValue<Vector2>();

            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (stick.x > 0.1 || stick.x < -0.1 || stick.y > 0.1 || stick.y < -0.1)
            {
                mouseWorldPosition = transform.position + new Vector3(stick.x, stick.y, 0);
            }

            mouseWorldPosition.z = 0;
            return mouseWorldPosition;
        }

    }
}
