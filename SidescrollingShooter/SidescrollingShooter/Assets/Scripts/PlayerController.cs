using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask floorLayerMask;

    [SerializeField]
    private int jumpHeight;

    [SerializeField]
    private float totalJumpTime;

    [SerializeField]
    private float speed;

    //[SerializeField]
    //private Animator animator;

    private float jumpTime;
    private bool isJumping = false;
    private Rigidbody2D playerRigidBody;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        jumpTime = totalJumpTime;
    }

    void Update()
    {        
        HandleJump();
        HandleMove();
        MaintainAir();
    }

    private void HandleMove()
    {
        if (Input.GetKey(KeyCode.A))
            playerRigidBody.velocity = new Vector2(-speed, playerRigidBody.velocity.y);
        else if (Input.GetKey(KeyCode.D))
            playerRigidBody.velocity = new Vector2(speed, playerRigidBody.velocity.y);
        else
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                isJumping = true;
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpHeight);
                //animator.SetBool("isJumping", isJumping);
            }
        }
    }

    private void EndJump()
    {
        isJumping = false;
        jumpTime = totalJumpTime;
    }

    private void MaintainAir()
    {
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTime >= 0)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpHeight);
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

        //if (!isJumping && IsGrounded())
            //animator.SetBool("isJumping", isJumping);
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
