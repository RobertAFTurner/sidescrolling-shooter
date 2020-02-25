using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask floorLayerMask;

    [SerializeField]
    private int jumpHeight;

    [SerializeField]
    private float boostSpeed;

    [SerializeField]
    private float totalJumpTime;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float boostTime;

    [SerializeField]
    private float startSpeed;

    [SerializeField]
    GameObject boostEffect;

    private float jumpTime;
    private bool isJumping = false;
    private Rigidbody2D playerRigidBody;
    private RigidbodyConstraints2D originalConstraints;
    private float storedYPos;
    private Vector3 storedVelocity;
    private float currentSpeed;
    private float boostTimer;
    private bool isBoosting = false;
    private bool canBoost = true;
    public bool IsBoosting => isBoosting;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        originalConstraints = playerRigidBody.constraints;

        jumpTime = totalJumpTime;
        boostTimer = boostTime;

        currentSpeed = startSpeed;
        //GlobalGameStats.platformSpeed = currentSpeed;
    }

    void Update()
    {        
        HandleJump();
        HandleBoost();
        MaintainAir();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded() || isBoosting)
            {
                EndBoost();
                isJumping = true;
                playerRigidBody.velocity = Vector2.up * jumpHeight;
                animator.SetBool("isJumping", isJumping);
            }
        }
    }

    private void HandleBoost()
    {
        if (IsGrounded())
            canBoost = true;

        if (Input.GetKeyDown(KeyCode.Return) && !isBoosting)
        {
            if (!canBoost)
                return;

            EndJump();

            //currentSpeed = GlobalGameStats.platformSpeed;
            //GlobalGameStats.platformSpeed = currentSpeed * boostSpeed;

            storedVelocity = playerRigidBody.velocity;
            storedYPos = playerRigidBody.gameObject.transform.position.y;

            isBoosting = true;
            canBoost = false;

            boostEffect.SetActive(true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isBoosting", isBoosting);

            //AudioManagerController.Instance.PlaySound("Boost");
        }

        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer < 0)
            {
                EndBoost();
            }
        }
    }

    private void EndBoost()
    {
        if (isBoosting)
        {
            //GlobalGameStats.platformSpeed = currentSpeed;
            isBoosting = false;
            boostTimer = boostTime;

            playerRigidBody.velocity = new Vector2(0, -1.5f); // Yuk!

            boostEffect.SetActive(false);
            animator.SetBool("isBoosting", false);
        }
    }

    private void EndJump()
    {
        isJumping = false;
        jumpTime = totalJumpTime;
    }

    private void MaintainAir()
    {
        if (isBoosting)
            playerRigidBody.gameObject.transform.position = new Vector3(playerRigidBody.gameObject.transform.position.x, storedYPos);

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTime >= 0)
            {
                playerRigidBody.velocity = Vector2.up * jumpHeight;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            EndJump();
        }

        if (!isJumping && IsGrounded())
            animator.SetBool("isJumping", isJumping);
    }

    private bool IsGrounded()
    {
        float extraHeight = 0.3f;
        var collider = GetComponent<BoxCollider2D>();

        RaycastHit2D cast = Physics2D.BoxCast(collider.bounds.center,
                                              collider.bounds.size,
                                              0f,
                                              Vector2.down,
                                              extraHeight,
                                              floorLayerMask);

        return cast.collider != null;
    }
}
