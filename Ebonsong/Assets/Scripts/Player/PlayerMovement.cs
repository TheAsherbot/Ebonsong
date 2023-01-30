using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerNameSpace
{
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : MonoBehaviour
    {
        public bool isOnKite;
        public LayerMask GroundLayer;
        
        [SerializeField] private float speed = 4;
        [SerializeField] private float jumpHight = 16;
        [SerializeField] private float femaleAthleticsModifyer = 1.5f;
        [SerializeField] private float maleAthleticsModifyer = 1f;

        [Header("Slopes")]
        [SerializeField] private float maxSlopeAngle = 50f;
        [SerializeField] private float slopeCheckPosition = 0.1f;
        [SerializeField] PhysicsMaterial2D maxFriction;
        [SerializeField] PhysicsMaterial2D noFriction;

        private bool isOnSlope;
        private bool canWalkOnSlope;
        private float slopeDownAngle;
        private float slopeSideAngle;
        private Vector2 slopeNormalPerpendicular;
        private CapsuleCollider2D capsuleCollider;


        public bool IsGrounded
        {
            get;
            private set;
        }
        private float CurrentAthleticsModifyer
        {
            get
            {
                return (player.IsMale == true) ? maleAthleticsModifyer : femaleAthleticsModifyer;
            }
        }
        private float CurrentSpeed
        {
            get
            {
                return speed * CurrentAthleticsModifyer;
            }
        }

        private bool isJumping;
        private Vector2 moveDirection;
        private new Rigidbody2D rigidbody;
        private Player player;

        private void Awake()
        {
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            rigidbody = GetComponent<Rigidbody2D>();
            player = GetComponent<Player>();
        }

        private void Start()
        {
            player.InputActions.LandMovement.Jump.started += Jump_started;
            player.InputActions.LandMovement.Jump.canceled += Jump_canceled;
        }

        private void Update()
        {
            CheckIfIsJumping();
        }

        private void FixedUpdate()
        {
            SlopeCheck();
            Move();
        }

        public void ClimeLatter()
        {
            if (moveDirection.y > .1f)
            {
                rigidbody.velocity = Vector2.zero;
                transform.position += CurrentAthleticsModifyer * speed * Time.deltaTime * new Vector3(0, moveDirection.y);
            }
        }

        public void FinishedClimingLatter()
        {
            Vector2 moveDirection = player.InputActions.LandMovement.Move.ReadValue<Vector2>();
            if (moveDirection.y > .1f)
            {
                rigidbody.AddForce(new Vector2(0, jumpHight * CurrentAthleticsModifyer / 3), ForceMode2D.Impulse);
            }
        }

        public void GetOffKite()
        {
            isOnKite = false;
            player.InputActions.LandMovement.Enable();
            player.InputActions.KiteMovement.Disable();
        }

        private void CheckIfIsJumping()
        {
            if (rigidbody.velocity.y <= 0.0f)
            {
                isJumping = false;
            }
        }

        private void SlopeCheck()
        {
            Vector2 checkPosition = transform.position;

            SlopeCheckHorizontal(checkPosition);
            SlopeCheckVertical(checkPosition);
        }

        private void SlopeCheckHorizontal(Vector2 checkPosition)
        {
            RaycastHit2D slopehitFront = Physics2D.Raycast(checkPosition, transform.right, slopeCheckPosition, GroundLayer);
            RaycastHit2D slopehitBack = Physics2D.Raycast(checkPosition, -transform.right, slopeCheckPosition, GroundLayer);

            if (slopehitFront)
            {
                isOnSlope = true;
                slopeSideAngle = Vector2.Angle(slopehitFront.normal, Vector2.up);
            }
            else if (slopehitBack)
            {
                isOnSlope = true;
                slopeSideAngle = Vector2.Angle(slopehitBack.normal, Vector2.up);
            }
            else
            {
                isOnSlope = false;
                slopeSideAngle = 0.0f;
            }
        }

        private void SlopeCheckVertical(Vector2 checkPosition)
        {
            RaycastHit2D raycast = Physics2D.Raycast(checkPosition, Vector2.down, slopeCheckPosition, GroundLayer);

            if (raycast)
            {
                slopeNormalPerpendicular = Vector2.Perpendicular(raycast.normal).normalized;

                slopeDownAngle = Vector2.Angle(raycast.normal, Vector2.up);

                if (slopeDownAngle != 0)
                {
                    isOnSlope = true;
                }
                else
                {
                    isOnSlope = false;
                }

                Debug.DrawRay(raycast.point, slopeNormalPerpendicular, Color.red);
                Debug.DrawRay(raycast.point, raycast.normal, Color.green);
            }

            if (slopeDownAngle > maxSlopeAngle || slopeSideAngle > maxSlopeAngle)
            {
                canWalkOnSlope = false;
            }
            else
            {
                canWalkOnSlope = true;
            }

            if (isOnSlope && moveDirection.x < 0.1f && moveDirection.x > -0.1f && canWalkOnSlope)
            {
                rigidbody.sharedMaterial = maxFriction;
                capsuleCollider.sharedMaterial = maxFriction;
            }
            else
            {
                rigidbody.sharedMaterial = noFriction;
                capsuleCollider.sharedMaterial = noFriction;
            }
        }

        private void Move()
        {
            moveDirection = player.InputActions.LandMovement.Move.ReadValue<Vector2>();

            if (ChechIfIsGrounded() && !isOnSlope && !isJumping && canWalkOnSlope) 
            {
                MoveWhenIsGroundedIsNotOnSlopeIsNotJumpingAndCanWalkOnSlope();
            }
            else if (ChechIfIsGrounded() && isOnSlope && !isJumping && canWalkOnSlope)
            {
                MoveWhenIsGoundedIsOnSlopeIsNotJumpingAndCanWalkOnSlope();
            }
            else if (isOnKite)
            {
                MoveWhenKite();
            }
            else if (!ChechIfIsGrounded())
            {
                MoveWhenIsNotGrounded();
            }
        }

        private void MoveWhenIsGroundedIsNotOnSlopeIsNotJumpingAndCanWalkOnSlope()
        {
            if (moveDirection.x > 0.1 || moveDirection.x < -0.1)
            {
                rigidbody.velocity = new Vector2(moveDirection.x * speed * CurrentAthleticsModifyer, 0);
                rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x, -CurrentSpeed, CurrentSpeed), 0);
            }
            else
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x / 2, 0);
            }
        }

        private void MoveWhenIsGoundedIsOnSlopeIsNotJumpingAndCanWalkOnSlope()
        {
            if (moveDirection.x > 0.1 || moveDirection.x < -0.1)
            {
                rigidbody.velocity = new Vector2(-moveDirection.x * speed * slopeNormalPerpendicular.x * CurrentAthleticsModifyer, -moveDirection.x * speed * slopeNormalPerpendicular.y * CurrentAthleticsModifyer);
                rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x, -CurrentSpeed, CurrentSpeed), -moveDirection.x * speed * slopeNormalPerpendicular.y * CurrentAthleticsModifyer);
            }
            else
            {
                if (rigidbody.velocity.y > 0.1f)
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x / 2, 0);
                }
                rigidbody.velocity = new Vector2(rigidbody.velocity.x / 2, rigidbody.velocity.y);
            }
        }

        private void MoveWhenIsNotGrounded()
        {
            if (moveDirection.x > 0.1 || moveDirection.x < -0.1)
            {
                rigidbody.velocity = new Vector2(moveDirection.x * speed * CurrentAthleticsModifyer, rigidbody.velocity.y);
                rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x, -CurrentSpeed, CurrentSpeed), rigidbody.velocity.y);
            }
            else
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x / 2, rigidbody.velocity.y);
            }
        }

        private void MoveWhenKite()
        {
            player.InputActions.LandMovement.Disable();
            player.InputActions.KiteMovement.Enable();

            float xInput = player.InputActions.KiteMovement.Horizontal.ReadValue<float>();

            float xVelocity = 0f;
            float yVelocity = -5f;
            if (xInput >= 0.1f)
            {
                xVelocity = 6f;
            }
            else if (xInput <= -0.1f)
            {
                xVelocity = 0f;
                yVelocity = -2f;
            }
            rigidbody.velocity = new Vector2(xVelocity, yVelocity);

        }

        private void Jump_started(InputAction.CallbackContext context)
        {
            if (ChechIfIsGrounded() == true && canWalkOnSlope)
            {
                isJumping = true;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpHight * CurrentAthleticsModifyer);
            }
        }

        private void Jump_canceled(InputAction.CallbackContext context)
        {
            if (rigidbody.velocity.y > 0)
            {
                isJumping = false;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y / 2);
            }
        }

        private bool ChechIfIsGrounded()
        {
            IsGrounded = false;
            float circleRadius = 0.1f;
            IsGrounded = Physics2D.OverlapCircle(player.transform.position, circleRadius, GroundLayer);
            return IsGrounded;
        }

    }
}
