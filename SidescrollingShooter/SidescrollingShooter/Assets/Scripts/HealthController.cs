
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private int hitPoints = 1;    

    private void Update()
    {
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (hitPoints <= 0)
        {
            gameObject.GetComponent<EntityController>().HandleDeath();
        }
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
    }

    public int HitPointsRemaining() => hitPoints;
}
