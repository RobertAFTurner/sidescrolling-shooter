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

    public int ammoCount = 200;

    public int shotgunAmmoCount = 5;

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
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            localScale.y = -0.5f;
        }
            
        else
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            localScale.y = 0.5f;
        }
            
        aimTransform.localScale = localScale;
    }

    private void HandleShoot()
    {

        if (ammoCount > 0)
            HandleShootMachineGun();
        else
            HandleSingleShot();

        if(shotgunAmmoCount > 0)
            HandleShotgun();
    }

    private void HandleShotgun()
    {
        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < 8; i++)
            {
                var randomNumber = UnityEngine.Random.Range(-1f, 1f);
                ShootWeapon(randomNumber);
            }

            shotgunAmmoCount--;
        }
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
                ammoCount--;
            }
        }
    }

    private void HandleSingleShot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ShootWeapon();                     
        }
    }

    private void ShootWeapon(float offset = 0f)
    {
        animator.SetTrigger("Shoot");

        var direction = MousePosition.GetMouseWorldPosition(Input.mousePosition, Camera.main) - transform.position;

        direction = new Vector3(direction.x, direction.y + offset);

        var ray = Physics2D.Raycast(gunTip.position, direction, 20f, enemyLayerMask);

        Debug.DrawRay(gunTip.position, direction, Color.red, 20f);
        Debug.Log(direction);
        if (ray.collider != null)
        {
            var hitEnemy = ray.collider.gameObject;
            var enemyController = hitEnemy.GetComponent<HealthController>();
            enemyController.TakeDamage(1);

            var effect = Instantiate(impactEffect, ray.point, new Quaternion());
            effect.transform.SetParent(hitEnemy.transform);
        }
    }

    public void AddAmmo(int count)
    {
        ammoCount += count;
    }

    internal void AddShotgunAmmo(int count)
    {
        shotgunAmmoCount += count;

        Debug.Log(shotgunAmmoCount);
    }
}
