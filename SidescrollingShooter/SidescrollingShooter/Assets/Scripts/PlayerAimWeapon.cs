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
        if(Input.GetMouseButtonDown(0))
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
    }
}
