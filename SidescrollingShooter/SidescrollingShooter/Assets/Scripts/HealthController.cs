using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    int hitPoints = 1;

    private void Update()
    {
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (hitPoints <= 0)
            Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
    }
}
