using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int hitPoints = 1;

    private void Update()
    {
        CheckForDeath();
    }

    public void TakeDamage(int damageTaken)
    {
        hitPoints -= damageTaken;
        Debug.Log(hitPoints);
    }

    private void CheckForDeath()
    {
        if (hitPoints <= 0)
            Destroy(gameObject);
    }
}
