using System;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform gunTip;

    [SerializeField]
    private Transform aimTransform;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private LayerMask enemyLayerMask;

    [SerializeField]
    private GameObject impactEffect;

    public bool gunUpgraded = false;

    private float shootTimer;

    [SerializeField]
    private float shootTime = 0.2f;

    private void Start()
    {
        shootTimer = shootTime;
    }

    void Update()
    {
        HandleAim();
        HandleShoot();
    }

    private void HandleAim()
    {
        var mousePos = MousePosition.GetMouseWorldPosition(Input.mousePosition, Camera.main);

        var aimDirection = (mousePos - transform.position).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        var localScale = new Vector3(0.5f, 0.5f, 1f);

        if (angle > 90 || angle < -90)
            localScale.y = -0.5f;
        else
            localScale.y = 0.5f;

        aimTransform.localScale = localScale;

    }

    private void HandleShoot()
    {

        if (gunUpgraded)
            HandleShootMachineGun();
        else
            HandleShootPistol();
    }

    private void HandleShootMachineGun()
    {
        if(Input.GetMouseButton(0))
        {
            shootTimer -= Time.deltaTime;
            if(shootTimer <= 0)
            {
                ShootWeapon();
                shootTimer = shootTime;
            }
        }
    }

    private void HandleShootPistol()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ShootWeapon();                     
        }
    }

    private void ShootWeapon()
    {
        animator.SetTrigger("Shoot");
        var ray = Physics2D.Raycast(gunTip.position, MousePosition.GetMouseWorldPosition(Input.mousePosition, Camera.main) - transform.position, 20f, enemyLayerMask);

        if (ray.collider != null)
        {
            var hitEnemy = ray.collider.gameObject;
            var enemyController = hitEnemy.GetComponent<HealthController>();
            enemyController.TakeDamage(1);

            var effect = Instantiate(impactEffect, ray.point, new Quaternion());
            effect.transform.SetParent(hitEnemy.transform);
        }
    }

    internal void SetAnimator(Animator newAnimator)
    {
        animator = newAnimator;
    }
}
