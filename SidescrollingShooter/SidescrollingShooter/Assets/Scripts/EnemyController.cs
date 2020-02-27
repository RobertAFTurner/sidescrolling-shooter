using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float attackDistance;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int attackStrength;
    
    private Rigidbody2D myRigidbody;

    private bool isAttacking;

    [SerializeField]
    private float attackSpeed;

    private float attackTimer;

    private void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RunAI();
    }

    private void RunAI()
    {
        if (!isAttacking)
            HandleMovement();
        else
            HandleAttack();
    }

    private void HandleMovement()
    {
        var currentDistanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);

        Debug.Log(currentDistanceFromPlayer);

        if (currentDistanceFromPlayer < attackDistance)
        {
            if (player.transform.position.x < transform.position.x)
                myRigidbody.velocity = new Vector2(-1 * speed, myRigidbody.velocity.y);
            else if (player.transform.position.x > transform.position.x)
                myRigidbody.velocity = new Vector2(1 * speed, myRigidbody.velocity.y);
        }
    }

    private void HandleAttack()
    {
        attackTimer -= Time.deltaTime;

        if(attackTimer < 0)
        {
            //animator.SetTrigger("Attack");
            player.GetComponent<HealthController>().TakeDamage(attackStrength);
            attackTimer = attackSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Attack");
            isAttacking = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("StopAttack");
            isAttacking = false;
            attackTimer = 0;
        }
    }
}
